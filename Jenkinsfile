pipeline {
    agent { label 'unity_windows' }
    environment {
         FTP_CRED = credentials('akromentos-deployment-ftp-auth')
         UNITY_LOCATION = "C:/Programs/Unity Editors/2019.4.2f1/Editor/Unity.exe"
         ARTIFACT_LOCATION = "G:/jenkins_workdir/artifact/gmtk-2020"
         UNITY_OPTIONS = "-nographics -batchmode -quit -executeMethod BuildScript.BuildCLI"
         FTP_ADDR = "192.168.0.20"
         WEBGL_DEPLOY_DIR = "/var/www/game-webgl/"
    }

    stages {
        stage('Build - Windows 32') {
            steps {
                bat '''
                    "%UNITY_LOCATION%" %UNITY_OPTIONS% -projectPath "%cd%/src/gmtk-jam-2020" -logFile "%ARTIFACT_LOCATION%/log_win.txt" -o "%ARTIFACT_LOCATION%" -buildTarget Win
                '''
            }
        }
        stage('Build - Windows 64') {
            steps {
                bat '''
                    "%UNITY_LOCATION%" %UNITY_OPTIONS% -projectPath "%cd%/src/gmtk-jam-2020" -logFile "%ARTIFACT_LOCATION%/log_win64.txt" -o "%ARTIFACT_LOCATION%" -buildTarget Win64
                '''
            }
        }
        stage('Build - WebGL') {
            steps {
                bat '''
                    "%UNITY_LOCATION%" %UNITY_OPTIONS% -projectPath "%cd%/src/gmtk-jam-2020" -logFile "%ARTIFACT_LOCATION%/log_webgl.txt" -o "%ARTIFACT_LOCATION%" -buildTarget WebGL
                '''
            }
        }
        stage('Deploying') {
            steps {
                bat '''
                    pscp -P 45796 -pw %FTP_CRED_PSW% -r %ARTIFACT_LOCATION%/webgl/ %FTP_CRED_USR%@%FTP_ADDR%:%WEBGL_DEPLOY_DIR%
                '''
            }
        }
    }
}