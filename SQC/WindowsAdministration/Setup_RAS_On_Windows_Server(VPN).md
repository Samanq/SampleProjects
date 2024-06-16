# Setup Remote Access Service RAS on windows server (VPN)
1. Go to the Server Manager and add a Role "Remote Access" an click the next button.
2. Check DirectAccess and VPN (RAS) and Routing in the "Select Role Services" step click install.
3. Close the installation and open the "Configure Remote Access" window.
4. Select "Deploy VPN Only".
5. Open "Route And Remote Access" console.
6. Right click on your server and click on "Configure and Enable Routing and remote Access.
7. Select Custom configuration. 
8. Check "VPN Access" and "LAN routing". (I'm not sure about these options)
9. Click next and finish.
10. Right click on your server and click on properties.
11. Go to the IPv4 tab and check the "Static adress pool"
12. Click add...
	Start IP address: 192.168.10.51
	End IP address: 192.168.10.60 
13. Open run and enter "Lusrmgr.msc" to Open the user manager console.
14. Create a new user, right click and the created user and go to the Dial-in tab and check "Allow Access".


15. Open WF.msc and go to the inbound rules, look for the "Secure Socket Tunneling Protocol (SSTP-In)" and enable it.
