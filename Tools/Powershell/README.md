# PowerShell

## Generating self-signed certificate

1. In powershell run to create the certificated:
```powershell
New-SelfSignedCertificate -Type Custom -Subject "CN=Caphyon, O=Caphyon, C=US" -KeyUsage DigitalSignature -FriendlyName "My Friendly Cert Name" -CertStoreLocation "Cert:\CurrentUser\My" -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}")

```
2. In powershell run to export the PFX file: 
```powershell
$certThumbprint = "ED18DC6B031562C7031C3FE7B940576D1733607E" `
$certLocation = "Cert:\CurrentUser\My\" `
$cert = Get-ChildItem -Path $certLocation -Thumbprint $certThumbprint `
$exportFilePath = "C:\path\to\exported\cert.pfx" `
$password = ConvertTo-SecureString -String "YourPfxPassword" -Force -AsPlainText `
Export-PfxCertificate -Cert $cert -FilePath $exportFilePath -Password $password `

```