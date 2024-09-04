#!/bin/bash

# NuGet Server URL
NUGET_SERVER_URL="http://your-nuget-server-url/api/v2/package"

# NuGet API Key
API_KEY="your-api-key"

# Paketlerin bulundu�u dizin
PACKAGE_DIR="./packages"

# Paketleri y�kleme
for package in "$PACKAGE_DIR"/*.nupkg; do
    echo "Uploading $package..."
    curl -X PUT "$NUGET_SERVER_URL" -H "X-NuGet-ApiKey:$API_KEY" -T "$package"
    echo "$package uploaded successfully."
done


# sudo apt-get install dos2unix
# sudo apt install awscli
# bu bir ubuntu sistemse setup olmam�� �eyleri de kurmal�y�z !
