🌱 Smart Greenhouse

Smart Greenhouse is a full-stack IoT application built around a RESTful API that follows industry best practices such as clean architecture, separation of concerns, DTO usage, and proper HTTP conventions.

The system is designed to monitor environmental conditions inside a greenhouse while providing centralized management of both sensor data and physical resources.



🧱 System Architecture

The project is composed of four main layers:

IoT Layer (ESP32 Controllers)

REST API (Back-end)

MVC Web Interfaces (Front-end)

MariaDB Database on Raspberry Pi



🔌 IoT & Sensors

ESP32 microcontrollers are used to collect data from multiple sensors, including:

Light intensity

Atmospheric pressure

Soil moisture

Air humidity

Sensor data is sent to the API using HTTP requests, enabling real-time data ingestion and centralized processing.



🌐 REST API

The back-end is a RESTful Web API responsible for:

Receiving and storing sensor data

Managing alerts and thresholds

Managing greenhouse resources (plants, sensors, zones)

Exposing data to the front-end applications

The API follows best practices:

REST-compliant endpoints

Proper HTTP status codes

DTO-based data exchange

Service and repository layers

Centralized error handling and logging



🖥️ MVC Web Interfaces

Two separate MVC front-end applications consume the API:


1️⃣ Data Management Interface

Used to monitor:

Sensor records

Environmental history

Alerts and critical events


2️⃣ Resource Management Interface

Used to manage:

Plants

Sensors

Zones

Resource configurations



🗄️ Database & Deployment

All data is stored in a MariaDB database running on a Raspberry Pi 3B+, acting as a lightweight and energy-efficient local server.
The API is the only component allowed to directly access the database, ensuring data integrity and security.



🎯 Project Goal

The goal of Smart Greenhouse is to provide a scalable, maintainable, and real-world IoT solution that demonstrates:

REST API design

IoT integration

Web application development

Edge computing with Raspberry Pi

Clean software architecture
