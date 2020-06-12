**Edgecam Manager** is a independent software that allow the user do things that perhaps was not possible inside the Hexagon Edgecam. 

The system is a addin, that runs over the windows and linux operation systems (without edgecam installed). This allow that others departaments can use this (to control/management inventory, sales, tools, machine hours and others.

The system is divided in the following modules:

## Users
The system allow create users from one or more organization units, assign system permissions (like delete, create, check departament requests, checkin, checkout).

The module allow manage each single user or a group of users from a different organization unit.

The system allow integrate with active directory (Windows AD) for user account control.

## Tasks
User can create tasks to himself or other users from others departaments, to reunions, tasks (like buy new stock of tools, stock of packing and others).

This module allow to, create a schedule for a day, week and month, to a specific user or a users group.

## Production Orders
User can manage and create a list of production orders, in other words, request a production for a specific part, assembly or a lot.

User can define the route and the process to production the items (like painting, machining, cleaning), dates to start and end of production, delivery date to final customer and among other things.

This module has a important feature: an A.I. (Artificial Intelligence) that control delivery dates and run automatically a script that controls Edgecam, apply machining strategies and generate the CNC if the delivery date is getting close.

## Quotes
User can create a quotetion about a product (part/assembly), service (part machining), job estimation (to machining a thousand parts) and among other things.

The quotation module allow user define ICMS, markup, profit, costs, additional costs, generate a reports to send to client customers.

This module is divided in three other sub modules: basic quote, advanced quote and technical quote.

>**Basic quote:** allow the user create a fast quotation, just including all items request by the customer and the price for each them.

>**Advanced quote:** allow the user create a quote with more information, like how long time will spend to machining a part (or an assembly), how many tools will used, which machines will use, how many process will be used during the machining, how many time will spend to create a instruction sequence and among other things.

>**Technical quote:** allow the user create a advanced quote but with more detail in costs (tax, currency, taxes, cancel reasons, and others).
User can control the expiration date from a quote, adding more costs to each day spend after .

## Inventory
This module allows the user control the any stock of the company, being a stock of tools, a stock of equipaments, a stock of paper, among others.

User can request a purchase order from a specific item, can make a inventory entry or inventory exit, control history modifications, quantity entries and so much more.

The user can too control the version of parts or assemblies (a product that contains a lot of versions, a product made inside the company, like furniture, tools).

The system have a auto request purchase order, if an item have the quantity less that the minimum quantity to work/sell, the system send a task/email to a defined user to purchase a quantity to complete the stock, to have the quantity always above the minimum.

## Shoop floor
This module can too, used to control machines in the shop floor, for example, time, production orders running, process, routes in coming (painting, cleaning).

The system have a integration with an app and web site that show the shop floor in real time.

## Integration
The system allow the user configure how will be want their integration with their systems (DAM, MAM, ERP, PIM, MRM, MDM, PDM). This integrations can be: *database linked server*, *web services*, *xml files*, *txt files* and so much more.

The system allow configure the time to run the integration (define the hour to sincronization, for example, each one minute, each thirty seconds), the sincronization time limit (timeout) and users to notify if sincronization ended with success or with error.

## Language
Currently, the system have two languages available:
>English

>Portuguese

>Spanish (in development in 2020 R2)
