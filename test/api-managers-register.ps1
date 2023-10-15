add-type @"
using System.Net;
using System.Security.Cryptography.X509Certificates;
public class TrustAllCertsPolicy : ICertificatePolicy {
    public bool CheckValidationResult(
        ServicePoint srvPoint, X509Certificate certificate,
        WebRequest request, int certificateProblem) {
        return true;
    }
}
"@
$AllProtocols = [System.Net.SecurityProtocolType]'Ssl3,Tls,Tls11,Tls12'
[System.Net.ServicePointManager]::SecurityProtocol = $AllProtocols
[System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy

$Body = @{
    secret_code = "my_secrete_code"
} | ConvertTo-Json
$Url = "http://localhost:32773/api/managers/register"
$Headers = @{
    'X-Manager-Login' = 'manageriniiped'
}

Invoke-WebRequest -Method Post -Uri $Url -Body $Body -ContentType "application/json" -Headers $Headers
