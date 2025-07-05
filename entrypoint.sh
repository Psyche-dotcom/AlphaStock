#!/bin/bash

# Export the Stripe secret key from Render's mounted secret file
if [ -f /etc/secrets/STRIPEKEY__TESTSECRETKEY ]; then
  export StripeKey__TestSecretKey=$(cat /etc/secrets/STRIPEKEY__TESTSECRETKEY)
fi

# Start the app
exec dotnet AlpaStock.Api.dll
