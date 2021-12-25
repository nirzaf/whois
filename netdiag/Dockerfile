FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base

RUN apt update && apt install -y tcpdump && \
    dotnet tool install -g dotnet-trace && \
    dotnet tool install -g dotnet-counters && \
    echo 'export PATH="$PATH:/root/.dotnet/tools"' >> /root/.bashrc

ENTRYPOINT ["/bin/bash"]
