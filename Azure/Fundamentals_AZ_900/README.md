# Azure Fundamentals  

Resources can have different regions than their resource group region.
The ResourceGroup region is just like the default region.

**Virtual network** is an isolated network in Azure.

We have have multiple VMs in the same subnet

Network Security Group is linked to Network Interface.

We can also attach a Network Security Group onto an entire subnet. (In means you can have multiple machines in a same Network Security Group)

We cannot assign a Network Security Group onto a Virtual Network.

The Network Security Group can only be attached to either Network Interfaces or onto  subnets.

In the Network Security Group (NSG) we can define inbound and outbound rules.

Application Security Group is an extension onto Network Security Group.

Application Security Group allows us to group multiple Virtual Machines together, and we can reference those groups in the Network Security Group to define rules for those Application Security Group.

By default two Virtual Networks cannot communicate to each other. This can be done by Network Virtual Peering.

Peering  is a feature under Virtual Networks.

Point-To-Site VPN and Site-To-Site VPNs.

Azure ExpressRoute lets you extend your on-premise network into the Microsoft cloud over a private connection with the help of a connectivity provider with ExpressRoute.

We can create a Load Balancer from resources.
Load Balancer is responsible for...
Select new Ip for the Load Balancer.
We need to add Health Probes to the Load balancer, in order to check the health of the servers.
Next we have to define the Load Balancing rules.

We can create a DNS Zone and link it to our external DNS server.
The name of the DNS Zone should be the same as the domain.
In the external DNS server we should add a record with IP of our load balancer to map the external DNS to the Azure DNS Zone.

---
## Azure Functions
...

## Microsoft Entra Cloud Sync
...

## Scaling
### Vertical Scaling
...
### Horizontal Scaling
...


## Storage

Blob storage is used to store large amounts of unstructured data, like images, video, audio and documents.

There are two type performance for a storage account. Standard and Premium, which has a lower latency.

With Azure File Service we can have a file share server.

Storage Queue Service

We can use Table Storage Service to store non-relational structured data.

There are 4 different Access Tiers for files in blob storage (container).
Those are Hot (Default), Cool, Cold, and Archive.


### Storage Costs
We have **storage costs** and **access costs**.<br> In the **Cool Access tier**, <ins>storage cost</ins> is <span style="color:green;">lower📉</span> but <ins>access cost</ins> is <span style="color:red;">higher📈</span>.

**Cool** access tier for a minimum **30** days.<br>
**Cold** access tier for a minimum **90** days.<br>
**Archive** access tier for minimum **180** days.<br>

When an object is <ins>archived</ins>, the object needs to be **rehydrated** before it can be reached.

We can define rules to change the object's access tier in Lifecycle management.

### There are 4 types of redundancy for Azure Storage
Locally redundant storage (LRS)<br>
3 copies of your objects will be created in the data center

Zone redundant storage (ZRS)<br>
Your data is going to replicated across 3 different Availability zones in the primary region.

Geo redundant storage (GRS)<br>
3 copies of your data will be created on 1 physical location using  LRS method, and at the same time another 3 copies will be created on another physical location in a different region.

Geo-zone-redundant storage (GZRS)<br>
Your data will be copied to 3 different availability zone in the primary region and also 3 copies will be created in a different region using LRS method.

We can also use a **file storage** as a **file share**, and map it into the computers.

We can use **Azure File Sync** to sync the files between our local file share server in our local network and the azure file share.

We can use **Table** to store **structured data**. for adding data to table we need to go to the **Storage Browser**.

You can use the **AzCopy** tool as the command line utility.

**Block blobs** is used to store **text** and **binary data**.

**Page blobs** are usually used for storing **virtual hard disks**.

For connecting to a storage account from a code we need to go the **Access Keys** section in the **Storage Account** and find the key and connectionString.

For transferring large amount of data (Terabytes) we can use **Azure data box** service.

We can use **service endpoints** for secure communication.

**Azure Cosmos DB** is a fully managed **NoSSQL** and **relational database** system.<br>
**Azure Cosmos DB** is a fully managed database service.<br> Here you don’t get any sort of access to the underlying database server.

You can host a **data warehouse** using the **Azure Synapse Analytics** service.


**Databricks** is a company that provides a platform for developing maintaining enterprise-grade data solutions such as, ingest data, Process data, Work with SQL, Data exploration, Machine learning, security and governance and virtualization.

---

## Concepts
Infrastructure as a service (IaaS)<br>
Example is: Virtual machine.

Platform as a service (PaaS)<br>
Example is Azure SQL Database.

Software as a service (SaaS)<br>
Example is Office 365.

## Cloud Model Types
There are three primary cloud deployment models.
- **Public Cloud**: Services are provided over the internet by third-party providers (e.g., Microsoft Azure, AWS, Google Cloud). These are accessible to anyone and offer scalability and flexibility and can be used with Pay as you go.

- **Private Cloud**: Infrastructure is dedicated to a **single organization**. It can be on-premises or hosted externally. Security and control are key benefits.

- **Hybrid Cloud**: Combines public and private clouds, allowing data and applications to move seamlessly between them. It offers flexibility and optimization.

---
## Azure Traffic Manager
This is a DNS based load balancer.<br>
We can distribute the traffic across the our resources.

We need to create **Traffic Manager Profile** from resources. 

## Azure Content Delivery Network (CDN)
It helps to distribute content across the world.

## Azure Test lab
We can create an automated workflow

## Azure Cognitive service
It can be used to manage vision and speech 

## Azure key vault
It allows you to store secrets, encryption keys and certificates.


## Permissions
**Owner:** Full access and able to delegate permission to other users. <br>
**Contributor**: Full access but, <span style="color: red">not</span> able to delegate permission to other users. <br>
**User Access:** Can delegate permission to other users. <br>
**Reader:** Can read the properties. <br>

## Guiding principles of Zero Trust
**Verify explicitly:**  Always authenticate and authorize based on all available data points.<br>

**Use least privilege access:**  Limit user access with Just-In-Time and Just-Enough-Access (JIT/JEA), risk-based adaptive policies, and data protection.<br>

**Assume breach:** Minimize blast radius and segment access. Verify end-to-end encryption and use analytics to get visibility, drive threat detection, and improve defenses.<br>


## Cost management
### Price calculator
... 

### Total Cost of Ownership Calculator (TCO)
Estimate the cost saving you can realize by migration your workloads to Azure.

## Service Level Agreement (SLA)
Is a commitment from Microsoft to its customers, detailing the performance and reliability standards they can expect from Azure services. Here are some key aspects of the Azure SLA:

- **Reliability and Availability**: It guarantees certain levels of service availability and performance.
- **Service Quality**: Outlines the level of service quality users can expect from Azure.
- **Compensation**: Specifies the compensation customers are entitled to if Azure fails to meet these commitments.

## Azure support plans
...

## Azure Hybrid benefit
With Azure Hybrid Benefit, if you have existing SQL Server or Windows Server licenses , you can get discounts when it comes to the costing for your Azure virtual machines.


## Resource Lock
Azure Resource Locks are a security feature designed to prevent accidental deletion or modification of Azure resources. <br/>
You can apply to Azure resources, like **subscriptions**, **resource groups**, or **individual resources**<br/>
There are two main lock levels you can implement:

- **Delete Lock (CanNotDelete)**: This lock allows authorized users to access and modify the resource but prevents them from deleting it.
- **Read-Only Lock (ReadOnly)**: This lock grants read-only access to authorized users, meaning they can view the resource but cannot modify or delete it.

## Azure Blueprints
...

## Azure Cloud shell
...

## Azure Arc
...


## Azure Advisor
Is a cloud assistant that can gives recommendation to improve the **cost effectiveness**, **performance**, **reliability**. and **security** of Azure resources.

## Azure Service Health
Is a suite of experiences that provides personalized **alerts** and **guidance** when issues in Azure services affect you. It notifies you about **Azure service incidents** and **planned maintenance**, so you can take action to mitigate downtime.

## Azure Monitor Service
...

## Azure Log Analytics
...

## Azure Application Insights
...



## Tips
 Data coming into Azure is free, but, data going out of Azure has cost.<br>
 Transferring data from one region to another has a cost as well


Continue from 207


---
Next we can sgo through Microsoft Certified: Azure Developer Associate (AZ-204),  and Designing and Implementing Microsoft DevOps Solutions (AZ-400)