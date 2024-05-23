# WeatherService - Test task

## What does the service do?

Service can get actual data of weather in Kazan<br/>
Every minute it's fetching data from accuweather.com API, and store in Database,<br/>
then we can get last 10 actual weather infos<br/>

## How it is works?

![image](https://github.com/s1ches/WeatherService/assets/121990701/550e124c-bc2d-4be0-a36b-30a39b2795e9)

Service A every minute fetching weather from accuweather.com API and Produce data to Apache Kafka<br/>
Service B consume messages from Kafka and send gRPC request on ServiceC to add weather info in Database
Service C has service to add weather info in Database which calls by Service B, Service C it's RESTful API which has one method to get last ten weather records 

### ServiceC - RESTful API which was built in Clean Architecture with CQRS and DDD(with Anemic Domain models) patterns

![image](https://github.com/s1ches/WeatherService/assets/121990701/cf145b8a-29ef-4482-85bc-f10e15705e27)


### About layers:
- Presentation<br/>
![image](https://github.com/s1ches/WeatherService/assets/121990701/d745c52f-d7cf-4929-945b-e89de2ee3d60)

Presentation layer has 1 project Web Application(Web API), which has 1 controller and 1 method, which returns ten latest weather records

- Core<br/>
  ![image](https://github.com/s1ches/WeatherService/assets/121990701/976538eb-a4ef-4e52-b15e-669213775303)

Core layer(Business logic layer) has 2 projects:<br/>
- Domain - for Domain models<br/>
- Application - for BusinessLogic realization<br/>

- Infrastructure<br/>
    ![image](https://github.com/s1ches/WeatherService/assets/121990701/c7b0b29a-f27c-4369-8e65-022bdfc475c8)

Infrastructure layer for realization service and interfaces/abstraction which need Core layer:<br/>
- ServiceC.Infrastructure.Grpc - for realization gRPC services<br/>
- ServiceC.Infrastructure.Persistence - for DbContext, Migrations, Entities Configurations<br/>
    
### Workers: ServiceB and ServiceC

![image](https://github.com/s1ches/WeatherService/assets/121990701/dcc300ae-fd1d-4f36-b33e-694e54527096)

- ServiceA - worker which works with Hangfire and collect data from external api by Cron every minute and then produce it to Kafka<br/>
  ![image](https://github.com/s1ches/WeatherService/assets/121990701/fe68a263-6013-4f00-82a7-8c3fcda30bba)
- ServiceB - worker which constantly works and consumes messages from Kafka, on every message ServiceB send gRPC request to a WeatherInteractionService to add data in Database<br/>
  ![image](https://github.com/s1ches/WeatherService/assets/121990701/d398ab79-29c6-497a-a57e-23ccbf144eb2)
- Common - contains protobuf file for workers<br/>
  ![image](https://github.com/s1ches/WeatherService/assets/121990701/4d4134b7-5c4e-428a-9ae5-bc7dda16be9f)


## How to Run the application

1. You need to up the docker-compose.yaml file with Kafka, Zookeeper and Shema-Registry
![image](https://github.com/s1ches/WeatherService/assets/121990701/26c99018-9f6d-4676-9ae2-c0f9323d6c2b)

docker-compose:
```
version: "3.7"
services:
  zookeeper:
    restart: always
    image: docker.io/bitnami/zookeeper:3.8
    ports:
      - "2181:2181"
    volumes:
      - "zookeeper-volume:/bitnami"
    environment:
      - ALLOW_ANONYMOUS_LOGIN=yes
  kafka:
    restart: always
    image: docker.io/bitnami/kafka:3.3
    ports:
      - "9093:9093"
    volumes:
      - "kafka-volume:/bitnami"
    environment:
      - KAFKA_BROKER_ID=1
      - KAFKA_CFG_ZOOKEEPER_CONNECT=zookeeper:2181
      - ALLOW_PLAINTEXT_LISTENER=yes
      - KAFKA_CFG_LISTENER_SECURITY_PROTOCOL_MAP=CLIENT:PLAINTEXT,EXTERNAL:PLAINTEXT
      - KAFKA_CFG_LISTENERS=CLIENT://:9092,EXTERNAL://:9093
      - KAFKA_CFG_ADVERTISED_LISTENERS=CLIENT://kafka:9092,EXTERNAL://localhost:9093
      - KAFKA_CFG_INTER_BROKER_LISTENER_NAME=CLIENT
    depends_on:
      - zookeeper
  schema-registry:
    image: confluentinc/cp-schema-registry:latest
    depends_on:
      - zookeeper
      - kafka
    ports:
      - "8035:8035"
    environment:
      SCHEMA_REGISTRY_KAFKASTORE_BOOTSTRAP_SERVERS: PLAINTEXT://kafka:9092
      SCHEMA_REGISTRY_HOST_NAME: schema-registry
      SCHEMA_REGISTRY_LISTENERS: http://0.0.0.0:8035
volumes:
  kafka-volume:
  zookeeper-volume:
```

2. In ServiceA in appsettings.json you need to replace WeatherApiConfig(WeatherApiKey) DefaultConnection(Username, Host and Password) with yours and create WorkerWeatherCollectorDb in postgres
3. In ServiceC in appsettings.json you need to replace DefaultConnection(Username, Host and Password) and apply migrations

After that u can run services A, B and C in any order you want
