
# 🐰 RabbitMQ + MassTransit Enterprise PoC
## Event-Driven Messaging Architecture with .NET 8

---

## 📌 Executive Summary

This repository demonstrates an enterprise-grade Proof of Concept (PoC) for implementing asynchronous messaging using:

- **RabbitMQ** as the message broker
- **MassTransit** as the abstraction layer
- **.NET 8** for API and Worker services
- **Docker** for local infrastructure setup

The purpose of this project is to provide a clean, scalable foundation for building microservices-based systems using event-driven communication patterns.

This solution is intentionally designed to mirror real-world fintech and distributed system architecture patterns.

---

# 🏗 Architecture Overview

## High-Level Flow

1. Client sends HTTP request to Publisher API
2. API publishes `NotificationRequested` event
3. RabbitMQ routes the message to the appropriate queue
4. Consumer Worker processes the event
5. Message is acknowledged (ACK) and removed from queue

## Architecture Components

- **Publisher.Api** → ASP.NET Core Web API (Event Producer)
- **Consumer.Worker** → Background Worker Service (Event Consumer)
- **Shared.Contracts** → Shared message contracts
- **RabbitMQ** → Message broker (Docker container)

---

# 📂 Solution Structure

RabbitMqMassTransit.Poc
│
├── Publisher.Api
│   ├── Controllers
│   ├── Program.cs
│   └── appsettings.json
│
├── Consumer.Worker
│   ├── Consumers
│   ├── Program.cs
│   └── appsettings.json
│
├── Shared.Contracts
│   └── Events
│
└── docker-compose.yml

---

# 🛠 Technology Stack

| Component | Technology |
|-----------|------------|
| Backend Framework | .NET 8 |
| Messaging Library | MassTransit |
| Message Broker | RabbitMQ |
| Containerization | Docker |
| API Documentation | Swagger |

---

# 🚀 Getting Started

## 1️⃣ Prerequisites

- .NET 8 SDK
- Docker Desktop
- Git

---

## 2️⃣ Clone Repository

```bash
git clone https://github.com/your-username/RabbitMqMassTransit.Poc.git
cd RabbitMqMassTransit.Poc
```

---

## 3️⃣ Start RabbitMQ

```bash
docker compose up -d
```

RabbitMQ Management UI:

http://localhost:15672

Credentials:

guest / guest

---

## 4️⃣ Run Consumer Worker

```bash
dotnet run --project Consumer.Worker
```

---

## 5️⃣ Run Publisher API

```bash
dotnet run --project Publisher.Api
```

Swagger:

https://localhost:<port>/swagger

---

# 📬 API Endpoints

## Publish Single Event

POST /api/notifications/request

Body:

{
  "channel": "sms",
  "to": "+96170123456",
  "message": "Hello from RabbitMQ + MassTransit!"
}

---

## Stress Test Endpoint

POST /api/notifications/stress?count=50000

Publishes bulk events asynchronously to simulate production-level load.

---

# 🔍 Observability via RabbitMQ UI

Navigate to:

http://localhost:15672

Monitor:

- Ready messages
- Unacked messages
- Publish rate
- Deliver rate
- Consumer utilization

---

# ⚙ Enterprise Messaging Concepts Demonstrated

## ✔ Producer / Consumer Separation
## ✔ Exchange & Queue Binding
## ✔ Automatic Queue Provisioning (MassTransit)
## ✔ Competing Consumers Pattern
## ✔ Stress & Load Behavior
## ✔ Failure Simulation
## ✔ Error Queue Handling
## ✔ Horizontal Scaling Simulation

---

# 🧪 Failure Simulation

Inside Consumer:

if (Random.Shared.Next(0, 10) == 0)
{
    throw new Exception("Random failure");
}

Failed messages move to:

consumer-worker_error

---

# 📈 Horizontal Scaling

Run multiple worker instances:

```bash
dotnet run --project Consumer.Worker
```

RabbitMQ distributes messages automatically across consumers.

---

# 🔐 Production Readiness Considerations

This PoC can be extended with:

- Retry policies (Immediate / Exponential)
- Delayed redelivery
- Idempotency validation
- Outbox pattern implementation
- Publisher confirms
- Quorum queues
- Observability integration (Prometheus / Grafana)
- Centralized logging (Serilog / ELK)

---

# 🎯 Learning Outcomes

After completing this PoC, you will understand:

- How RabbitMQ routes messages
- How MassTransit abstracts broker complexity
- How concurrency affects throughput
- How backpressure occurs
- How failures are handled
- How to scale message consumers

---

# 🏁 Conclusion

This project serves as a foundational blueprint for building reliable, scalable, event-driven systems using RabbitMQ and MassTransit.

It is structured to reflect enterprise architecture patterns commonly used in fintech and distributed systems.

---

## 📄 License

This project is provided for educational and demonstration purposes.
