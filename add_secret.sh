# #!/bin/bash

# export AWS_ACCESS_KEY_ID=test
# export AWS_SECRET_ACCESS_KEY=test
# export AWS_DEFAULT_REGION=eu-central-1

# add_secret() {
# 	secret_name=$1 
# 	secret_value=$2
# 	aws --endpoint-url=http://localhost:4566 secretsmanager create-secret --name $secret_name --secret-string $secret_value
# 	echo "Added secret $secret_name"
# }

# add_secret "MySecret1" "my_secret_1"
# add_secret "MySecret2" "my_secret_2"
# add_secret "MySecret3" "my_secret_3"
# add_secret "MySecret4" "my_secret_4"
# add_secret "MySecret5" "my_secret_5"

# # sudo apt-get install dos2unix
# # sudo apt install awscli
# # bu bir ubuntu sistemse setup olmam�� �eyleri de kurmal�y�z !

# --------------------------------------------------------------------------------------

#!/bin/bash

export AWS_ACCESS_KEY_ID=test
export AWS_SECRET_ACCESS_KEY=test
export AWS_DEFAULT_REGION=eu-central-1

add_secret() {
    environment=$1
    secret_name=$2 
    secret_value=$3
    aws --endpoint-url=http://localhost:4566 secretsmanager create-secret --name $secret_name --secret-string $secret_value --tags Key=Environment,Value=$environment
    echo "Added secret $secret_name for $environment"
}

add_secret "Development" "MySecret1" "my_secret_1"
add_secret "Development" "MySecret2" "my_secret_2"
add_secret "Development" "MySecret3" "my_secret_3"
add_secret "Development" "MySecret4" "my_secret_4"
add_secret "Development" "MySecret5" "my_secret_5"

# sudo apt-get install dos2unix
# sudo apt install awscli
# bu bir ubuntu sistemse setup olmam�� �eyleri de kurmal�y�z !