# 0.0.3

## Commit: <a href="https://github.com/s1ches/WeatherService/commit/33ae20028d3f8896a92be38117b8fb25c04f890b">#33ae200</a>

_Released 05/22/2024_

### Features:
- Create ServiceA
- Realize fetching Weather from accuweather.com API
- Realize Base Generic Apache Kafka message Producing 
- Realize Base Generic Serializer for Apache Kafka Message Values
- Combine services functional and realize Background Worker, using Hangfire

# 0.0.1 

## Commit: <a href="https://github.com/s1ches/WeatherService/commit/45d873c69aedcfb1af2f6a15f418f19aca3298f9">#45d873c</a>

_Released 05/21/2024_

#### Features:
- Create architecture for ServiceC
- Realize Domain Layer(create WeatherRecord, enums for it)
- Realize Persistence Layer(create DbContext and configure WeatherRecord)
- Realize Application Layer(create Query to get last ten weather records from db, add interfaces for db and GRPC service)
- Realize Presentation Layer(Create WeatherController with HttpGet method GetLastTenWeatherRecords)


