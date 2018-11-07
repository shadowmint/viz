# Viz

## Setup

    npm ci

## Dev

    npm start

in service folder:

    dotnet run --project ./Service

## Build

    npm build
    rm -r service/wwwroot
    cp -r build service/wwwroot

in service folder:

    cd Service
    dotnet publish -o [TARGET_OUTPUT_FOLDER]
    
to run, in output folder:

    dotnet Service.dll