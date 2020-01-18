def getImageTag(branchName) {
  return branchName == "master" ? "latest" : branchName;
}

pipeline {
  agent any

  environment {
    DOCKER_REPO = "docker.ptrampert.com"
    DOCKER_REPO_CREDENTIALS = "nexus"
    DOTNET_BUILD_IMAGE = "mcr.microsoft.com/dotnet/core/sdk:3.1"
    NODE_BUILD_IMAGE = "node:lts"
    IMAGE_TAG = getImageTag(BRANCH_NAME)
  }

  options {
    buildDiscarder(logRotator(numToKeepStr:'5'))
  }

  stages {
    stage('Test') {
      parallel {
        /*
        stage("NUnit") {
          agent {
            docker {
              image DOTNET_BUILD_IMAGE
              reuseNode true
              args "-e HOME=$HOME"
            }
          }
          steps {
            sh 'echo $HOME'
            sh "touch TCloud/WebpackResources.json"
            sh "dotnet test TCloud.Test/TCloud.Test.csproj -l trx /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura"
          }
        }
        */

        stage("Jasmine") {
          agent {
            docker {
              image NODE_BUILD_IMAGE
              reuseNode true
              args "-e HOME=$HOME"
            }
          }
          steps {
            dir('TCloud/ClientApp') {
              sh 'echo $HOME'
              sh 'npm install'
              sh 'npm run test-once'
            }
          }
        }
      }
      post {
        always {
          step([
            $class: 'XUnitBuilder',
            testTimeMargin: '60000',
            thresholdMode: 1,
            thresholds: [
              [
                $class: 'FailedThreshold',
                failureNewThreshold: '',
                failureThreshold: '',
                unstableNewThreshold: '',
                unstableThreshold: '0'
              ],
              [
                $class: 'SkippedThreshold',
                failureNewThreshold: '',
                failureThreshold: '',
                unstableNewThreshold: '',
                unstableThreshold: ''
              ]
            ],
            tools: [
              [
                $class: 'MSTestJunitHudsonTestType',
                deleteOutputFiles: true,
                failIfNotNew: false,
                pattern: '**/*.trx',
                skipNoTestFiles: true,
                stopProcessingIfError: true
              ],
              [
                $class: 'JUnitType',
                deleteOutputFiles: true,
                failIfNotNew: true,
                pattern: 'TCloud.Web/ClientApp/testResults/*.xml',
                skipNoTestFiles: false,
                stopProcessingIfError: true
              ]
            ]
          ])
          cobertura(
            autoUpdateHealth: false,
            autoUpdateStability: false,
            coberturaReportFile: '**/*cobertura*.xml',
            conditionalCoverageTargets: '70, 0, 0',
            failUnhealthy: false,
            failUnstable: false,
            lineCoverageTargets: '80, 0, 0',
            maxNumberOfBuilds: 0,
            methodCoverageTargets: '80, 0, 0',
            onlyStable: false,
            sourceEncoding: 'ASCII',
            zoomCoverageChart: false
          )
        }
      }
    }

    stage("Build Web Docker Image") {
      agent any
      steps {
        withDockerRegistry(url: "https://${DOCKER_REPO}", credentialsId: DOCKER_REPO_CREDENTIALS) {
          sh "docker build -t ${DOCKER_REPO}/tcloud-web:${IMAGE_TAG} -f TCloud.Web/Dockerfile ."
          sh "docker push ${DOCKER_REPO}/tcloud-web:${IMAGE_TAG}"
          sh "docker rmi ${DOCKER_REPO}/tcloud-web:${IMAGE_TAG}"
        }
      }
    }

    stage("Build Api Docker Image") {
      agent any
      steps {
        withDockerRegistry(url: "https://${DOCKER_REPO}", credentialsId: DOCKER_REPO_CREDENTIALS) {
          sh "docker build -t ${DOCKER_REPO}/tcloud-api:${IMAGE_TAG} -f TCloud.Api/Dockerfile ."
          sh "docker push ${DOCKER_REPO}/tcloud-api:${IMAGE_TAG}"
          sh "docker rmi ${DOCKER_REPO}/tcloud-api:${IMAGE_TAG}"
        }
      }
    }
  }
  post {
    changed {
      mail to: 'paul.trampert@gmail.com', subject: "Build status of ${env.JOB_NAME} changed to ${currentBuild.result}", body: "Build log may be found at ${env.BUILD_URL}"
    }
    always {
      deleteDir()
      sh "docker system prune -f"
    }
  }
}