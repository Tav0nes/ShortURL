# Certificate Verification and Docker Troubleshooting

## Steps to Verify and Import Certificate on Windows

### Open MMC:
1. Press `Win + R` to open the Run dialog.
2. Type `mmc` and press Enter to open the Microsoft Management Console.

### Add the Certificates Snap-in:
1. Go to `File > Add/Remove Snap-in...`.
2. Select `Certificates` from the list and click `Add`.
3. Choose `Computer account` (to manage certificates for all users on this computer) or `My user account` (to manage your user certificates), then click `Next` and `Finish`.

### Navigate to Certificate Store:
1. Expand the `Certificates (Local Computer)` node.
2. Navigate to the `Trusted Root Certification Authorities > Certificates` folder.

### Import Certificate:
1. Right-click on `Certificates` under `Trusted Root Certification Authorities`.
2. Select `All Tasks > Import`.
3. Follow the wizard to import your `apiserver.crt` file.

**Note:** You may need to reboot your computer for changes to take effect.

## Importing a Self-Signed Certificate in Firefox

### Import as a Certificate Authority (CA) (Not Recommended for Self-Signed):
- **Warning:** This method is not secure for importing a self-signed certificate as a CA. Firefox expects a CA certificate to sign other certificates. Instead, import it directly for trusted sites only.

### Adding to Firefox Trusted Certificates:
1. Open Firefox and go to `Settings`.
2. Scroll down to the `Privacy & Security` section.
3. Under `Certificates`, click `View Certificates`.
4. In the Certificate Manager, click the `Servers` tab.
5. Click `Add Exception`.
6. In the Add Security Exception dialog, enter `https://localhost:8081` and click `Get Certificate`.
7. Confirm the security exception to trust this certificate.

## Importing a Self-Signed Certificate in Chrome
1. Open Chrome and navigate to `chrome://settings/certificates`.
2. Go to the `Authorities` tab.
3. Click `Import` and select your `apiserver.crt` file.
4. Check `Trust this certificate for identifying websites`.

## Troubleshooting API with Docker

### To Check if the API Hits the Database:
Run the following commands from the project directory:

```sh
docker exec -it webapi-container bash
curl -v https://localhost:8081/api/urls --insecure
curl -v http://localhost:8080/api/urls
```

See what it curretly looks like:

![apihit](../images/apihit.png)