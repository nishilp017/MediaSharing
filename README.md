# MediaSharing


Developed Azure Cloud Service to achieve the Targeted funtinalities. 
Upload was verified to Upload file size of 30 MB. 

Technologies Used:

Azure Cloud Service
.Net 4.6.1
Azure Storage Tables(NoSql)
Azure Storage Blob(File Storage)
JavaScript for the UI Upload and Download functinalities 


Cloud Service Url: http://webrole120180618082139.azurewebsites.net/

UI Url : http://webrole120180618082139.azurewebsites.net/
Endpoints:

1. POST http://webrole120180618082139.azurewebsites.net/api/files for Upload 
2. GET http://webrole120180618082139.azurewebsites.net/api/files?id={} To Download file by ID
2. GET http://webrole120180618082139.azurewebsites.net/api/files To Get list of all uploaded files 

Deployment step:

NOTE: Need to to have VS 2017 installed with Azure Developer tools

Open the project, Go to Build And Publish WebRole.
Create new publish profile for ease. 


