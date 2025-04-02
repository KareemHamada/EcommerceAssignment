A complete RESTful API for e-commerce operations

Implementation using generic repository pattern and unit of work

Entity Framework Core for data access

FluentValidation for request validation

Proper HTTP status codes for all scenarios

Database seeding for products

Samples JSON requests for testing
  - Customers
  1- for create a customer
        POST =>  https://localhost:7201/api/customers
        {
            "name": "Kareem Hamada",
            "email": "kareemhamada219@gmail.com",
            "phone": "01123464360"
        }
  
  2- For get customers
      GET =>  https://localhost:7201/api/customers
  3- For get customer by id
      GET => https://localhost:7201/api/customers/1

  - Orders
    1- For create an order
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

    2- for get order by id
      GET => https://localhost:7201/api/orders/1

    3- For update order status
      PUT => https://localhost:7201/api/orders/status/1
      {
          "status": "Delivered"
      }
          
    
