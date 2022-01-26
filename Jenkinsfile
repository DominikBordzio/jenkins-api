pipeline {    
  environment {
      dockerImage=''
      registry = "twwoapi/twwo-api"
      registryCredential = "twwoapi"
  }    
  agent any
  stages {
      stage('Clone repository') {
          steps {
            git branch: 'main', url: 'https://github.com/DominikBordzio/jenkins-api.git'
          }
      }
      stage('Build image') {
          steps {
            script {
                dockerImage = docker.build("twwoapi/twwo-api:latest", "-f ./vagrant2-api/Dockerfile ./vagrant2-api")  
            }
          }
        }
        stage('Deploy image') {
            steps {
                script {
                    docker.withRegistry('', registryCredential){
                        dockerImage.push("latest")
                        dockerImage.push("$BUILD_NUMBER")
                    }
                }
            }
        }
   }
}
