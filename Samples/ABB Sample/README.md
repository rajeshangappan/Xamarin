**Store Management**

Goal:

- Maintain the incoming and outgoing stocks.
- Mange the stock&#39;s available quantity and cost.
- Monitoring the Staff in/out details and their activity.

**Front End:**

- Implementing the Mobile application using Xamarin.Forms.

Here, we have implement the application for below roles.

- Store manager
  - Manage the store stocks.
  - Add or Delete the product in store and update the product details.
  - Maintain the staffs.
- Store Staffs
  - Maintain the store product units in and out details.

UI Screens:

- Login
- Main Page
  - Show the Module items depend on the Role.
- Product Page
  - It will show the list of Products
- Product Detail Page
  - It will show the product detail and give the option the edit/add the products.

Offline Sync: (Background Sync.cs will take care)

- For Offline we majorly used the below columns in SQLite
  - **Version** – Using this, we will sync the data and override the new data to both offline and online.
  - **UpdatatedAt** – Using this column we will override the latest data to both offline and online
  - **IsDeleted** (soft delete)– Using this column will manage the deleted data in both offline and online.

- We should remove the &quot;isdeleted = true&quot; columns frequently depends on the customer requirement.

Note: Offline data should be clear depends on specific time interval.

IOC Container:

- Implemented the IOC container to resolve the viewmodel, service and background objects.

**Middleware:**

- Created the Web API by .Net core.
- Here we majorly have below controllers
  - Login Controller – Manage the Login mechanism
  - Product Controller – Manage the product like Add, delete, update
  - SyncController – Sync the offline data to online and vice versa.
- Added the authorization filter to validate the user and user roles.

BackEnd:

- Store the backend data in SQL Server.
- Here we used the 2 tables user and product

Output:

# Output

![Store App](https://github.com/rajeshangappan/Xamarin/blob/master/Samples/ABB%20Sample/Store.gif.gif)
