FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Print the current working directory
RUN echo $PWD

# Copy project files, excluding bin and obj folders
COPY ./CV.Authentication.Presentation/*.csproj ./CV.Authentication.Presentation/
COPY ./CV.Authentication.Domain/*.csproj ./CV.Authentication.Domain/
COPY ./CV.Authentication.Application/*.csproj ./CV.Authentication.Application/
COPY ./CV.Authentication.AccessData/*.csproj ./CV.Authentication.AccessData/

# print the directory for debugging
RUN echo $PWD

# Restore dependencies
RUN dotnet restore ./CV.Authentication.Presentation/CV.Authentication.Presentation.csproj

# Copy the rest of the source code, excluding bin and obj folders
COPY . ./
RUN find . -name bin -o -name obj | xargs rm -rf

# Build the application
RUN dotnet build ./CV.Authentication.Presentation/CV.Authentication.Presentation.csproj

# Change directory to the main project folder
WORKDIR /app

# Print the contents of the output folder for debugging
RUN find . -name "out"

# Publish the application
RUN dotnet publish ./CV.Authentication.Presentation/CV.Authentication.Presentation.csproj -o /app/publish

# Print the contents of the publish folder for debugging
RUN find /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published output from the build image
COPY --from=build /app/publish ./

# Expose the port
EXPOSE 80

# Set timezone
ENV TZ=America/Argentina/Buenos_Aires
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Set the entry point
ENTRYPOINT ["dotnet", "CV.Authentication.Presentation.dll"]
