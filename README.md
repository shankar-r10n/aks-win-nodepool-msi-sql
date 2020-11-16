# .NET 4.7 ASP.Net MVC App on AKS Windows Node Pool + Azure SQL DB + User-assigned managed identity  

This repo provides the prerequisites, setup instructions and a sample .NET 4.7.2 ASP.NET MVC App for testing Azure SQL DB connectivity utilizing User-Assigned Managed Identities on AKS cluster with a Windows node pool.

## Premise

The assignment of a user-assigned managed identity (uMI) to a Windows Node pool and creating a DB user for the uMI in a Azure SQL DB (say AlphaDB) - should ensure that an app hosted on the Windows node pool **can connect** to the Alpha DB. 

And it should also ensure that the  same app **cannot connect** to another DB (say Gamma DB) in the same Azure SQL Logical server which **does not** have the DB user created for the uMI.

## Logical Diagram
![](https://github.com/shankar-r10n/aks-win-nodepool-msi-sql/blob/main/img/LogicalView.PNG)

## :memo: Prerequisites
- AKS Cluster for Windows Nodes (running an actively supported Kubernetes version.) This setup  has been tested with AKS Clusters with Kubernetes v 1.17.11 and v 1.18.10
- A Windows node pool in the AKS cluster.
- Container registry (like Azure Container Registry for storing / managing the sample app image.)
- 2 Azure SQL DBs (say *[AlphaDB] and [GammaDB]*) in an Azure SQL server.


## Setup Instructions

1.	Create an user-assigned managed identity.
```
az identity create -g <RESOURCE GROUP> -n <USER ASSIGNED IDENTITY NAME>
```
2.	Assign the created identity to the Windows node pool VMSS in respective **MC_** resource group of the AKS cluster.
```
az vmss identity assign -g <RESOURCE GROUP>
-n <VIRTUAL MACHINE SCALE SET NAME>
--identities <USER ASSIGNED IDENTITY>
```
3. As an AAD admin for the Azure SQL server,  create a user in Azure SQL DB – [Alpha DB]– for the newly created managed identity.
```
CREATE USER [<identity-name>] FROM EXTERNAL PROVIDER
```

4.	In the sample app - NPTester - `HomeController.cs` reads the env vars for connection strings as set during deployment; see sample winnp.yaml as follows.
5.	Build/push to container registry and deploy the app to the AKS Cluster.
> A sample yaml file - winnp.yaml - is provided in the repo for deploying the app and creating a backing service. This also sets the needed environment variables where you can provide the DB connection strings valuses and the AppId value which is the clientId of the newly created managed identity.
6.	Launch the app and verify that connection to Alpha DB is successful.
7.	Verify that connection to Gamma DB is NOT successful.


## Result

The app  should reflect similar to following image - depicting ability to connect to Alpha DB. And unsuccessful connnection to GammaDB.
![](https://github.com/shankar-r10n/aks-win-nodepool-msi-sql/blob/main/img/AppResultScreenshot.PNG)

