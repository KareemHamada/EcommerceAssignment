A complete RESTful API for e-commerce operations

Implementation using generic repository pattern and unit of work

Entity Framework Core for data access

FluentValidation for request validation

Proper HTTP status codes for all scenarios

Database seeding for products

Samples JSON requests for testing
  - Customers
    - To create a customer
          POST =>  https://localhost:7201/api/customers
          {
              "name": "Kareem Hamada",
              "email": "kareemhamada219@gmail.com",
              "phone": "01123464360"
          }
    
    - To get customers
        GET =>  https://localhost:7201/api/customers
    - To get customer by ID
        GET => https://localhost:7201/api/customers/1

  - Orders
    - To create an order
      POST =>  https://localhost:7201/api/orders
        {
            "customerId": 1,
            "items": [
                {
                    "productId": 1,
                    "quantity": 2
                },
                {
                    "productId": 2,
                    "quantity": 1
                }
            ]
        }

    - To get order by ID
      GET => https://localhost:7201/api/orders/1

    - to update order status
      PUT => https://localhost:7201/api/orders/status/1
      {
          "status": "Delivered"
      }
          
    
