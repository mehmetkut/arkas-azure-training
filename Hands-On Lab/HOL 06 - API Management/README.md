Overview

This lab shows you how to quickly get started in using Azure API Management and make your first API call.

# What is Azure API Management?

You can use Azure API Management to take any backend and launch a full-fledged API program based on it.

Common scenarios include:

-   **Securing mobile infrastructure** by gating access with API keys, preventing DOS attacks by using throttling, or using advanced security policies like JWT token validation.

-   **Enabling ISV partner ecosystems** by offering fast partner onboarding through the developer portal and building an API facade to decouple from internal implementations that are not ripe for partner consumption.

-   **Running an internal API program** by offering a centralizedlocation for the organization to communicate about the availability and latest changes to APIs, gating access based on organizational accounts, all based on a secured channel between the API gateway and the backend.


The system is made up of the following components:

-   The **API gateway** is the endpoint that:

    -   Accepts API calls and routes them to your backends.

    -   Verifies API keys, JWT tokens, certificates, and other credentials.

    -   Enforces usage quotas and rate limits.

    -   Transforms your API on the fly without code modifications.

    -   Caches backend responses where set up.

    -   Logs call metadata for analytics purposes.

-   The **publisher portal** is the administrative interface where you set up your API program. Use it to:

    -   Define or import API schema.

    -   Package APIs into products.

    -   Set up policies like quotas or transformations on the APIs.

    -   Get insights from analytics.

    -   Manage users.

-   The **developer portal** serves as the main web presence for developers, where they can:

    -   Read API documentation.

    -   Try out an API via the interactive console.

    -   Create an account and subscribe to get API keys.

    -   Access analytics on their own usage.

# Create an API
Management instance

The first step in working with API Management is to create a service
instance. Sign in to the [*Azure Portal*](https://portal.azure.com/) and
click **New**, **Web + Mobile**, **API Management**.

<img src="./media/image1.png" width="599" height="304" />

For **Name**, specify a unique sub-domain name to use for the service
URL.

Choose the desired **Subscription**, **Resource
group** and **Location** for your service instance.

Enter **Contoso Ltd.** for the **Organization Name**, and enter your
email address in the **Administrator E-Mail** field.

Note

This email address is used for notifications from the API Management
system. For more information, see [*How to configure notifications and
email templates in Azure API
Management*](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-configure-notifications).

<img src="./media/image2.png" width="599" height="513" />

API Management service instances are available in three tiers:
Developer, Standard, and Premium.

Click **Create** to start provisioning your service instance.

<img src="./media/image3.png" width="599" height="265" />

Once the service instance is created, the next step is to create or
import an API.

<span id="create-api" class="anchor"></span>Import an API

An API consists of a set of operations that can be invoked from a client
application. API operations are proxied to existing web services.

APIs can be created (and operations can be added) manually, or they can
be imported. In this lab, we will import the API for a sample calculator
web service provided by Microsoft and hosted on Azure.

APIs are configured from the publisher portal. To reach it,
click **Publisher portal** from the service toolbar.

<img src="./media/image4.png" width="599" height="139" />

To import the calculator API, click **APIs** from the **API
Management** menu on the left, and then click **Import API**.

<img src="./media/image5.png" width="599" height="403" />

Perform the following steps to configure the calculator API:

1.  Click **From URL**,
     enter [***http://calcapi.cloudapp.net/calcapi.json***](http://calcapi.cloudapp.net/calcapi.json) into
     the **Specification document URL** text box, and click
     the **Swagger** radio button.

2.  Type **calc** into the **Web API URL suffix** text box.

3.  Click in the **Products (optional)** box and choose **Starter**.

4.  Click **Save** to import the API.

<img src="./media/image6.png" width="599" height="596" />

Note

**API Management** currently supports both 1.2 and 2.0 version of
Swagger document for import. Make sure that, even though [*Swagger 2.0
specification*](http://swagger.io/specification) declares
that host, basePath, and schemes properties are optional, your Swagger
2.0 document **MUST** contain those properties; otherwise it won't get
imported.

Once the API is imported, the summary page for the API is displayed in
the publisher portal.

<img src="./media/image7.png" width="599" height="436" />

The API section has several tabs. The **Summary** tab displays basic
metrics and information about the API. The *Settings* tab is used to
view and edit the configuration for an API. The *Operations* tab is used
to manage the API's operations. The **Security** tab can be used to
configure gateway authentication for the backend server by using Basic
authentication or *mutual certificate authentication*, and to
configure *user authorization by using OAuth 2.0*. The **Issues** tab is
used to view issues reported by the developers who are using your APIs.
The **Products** tab is used to configure the products that contain this
API.1

By default, each API Management instance comes with two sample products:

-   **Starter**

-   **Unlimited**

In this lab, the Basic Calculator API was added to the Starter product
when the API was imported.

In order to make calls to an API, developers must first subscribe to a
product that gives them access to it. Developers can subscribe to
products in the developer portal, or administrators can subscribe
developers to products in the publisher portal. You are an administrator
since you created the API Management instance in the previous steps in
the lab, so you are already subscribed to every product by default.

<span id="call-operation" class="anchor"></span>Call an operation from
the developer portal

Operations can be called directly from the developer portal, which
provides a convenient way to view and test the operations of an API. In
this lab step, you will call the Basic Calculator API's **Add two
integers** operation. Click **Developer portal** from the menu at the
top right of the publisher portal.

<img src="./media/image8.png" width="391" height="40" />

Click **APIs** from the top menu, and then click **Basic Calculator** to
see the available operations.

<img src="./media/image9.png" width="487" height="307" />

Note the sample descriptions and parameters that were imported along
with the API and operations, providing documentation for the developers
that will use this operation. These descriptions can also be added when
operations are added manually.

To call the **Add two integers** operation, click **Try it**.

<img src="./media/image10.png" width="599" height="482" />

You can enter some values for the parameters or keep the defaults, and
then click **Send**.

<img src="./media/image11.png" width="599" height="671" />

After an operation is invoked, the developer portal displays
the **Response status**, the **Response headers**, and any **Response
content**.

<img src="./media/image12.png" width="599" height="606" />

<span id="view-analytics" class="anchor"></span>View analytics

To view analytics for Basic Calculator, switch back to the publisher
portal by selecting **Manage** from the menu at the top right of the
developer portal.

<img src="./media/image13.png" width="180" height="156" />

The default view for the publisher portal is the **Dashboard**, which
provides an overview of your API Management instance.

<img src="./media/image14.png" width="599" height="447" />

Hover the mouse over the chart for **Basic Calculator** to see the
specific metrics for the usage of the API for a given time period.

Note

If you don't see any lines on your chart, switch back to the developer
portal and make some calls into the API, wait a few moments, and then
come back to the dashboard.

Click **View Details** to view the summary page for the API, including a
larger version of the displayed metrics.

<img src="./media/image15.png" width="539" height="136" />

<img src="./media/image16.png" width="599" height="443" />

For detailed metrics and reports, click **Analytics** from the **API
Management** menu on the left.

<img src="./media/image17.png" width="599" height="493" />

The **Analytics** section has the following four tabs:

-   **At a glance** provides overall usage and health metrics, as well
    as the top developers, top products, top APIs, and top operations.

-   **Usage** provides an in-depth look at API calls and bandwidth,
     including a geographical representation.

-   **Health** focuses on status codes, cache success rates, response
     times, and API and service response times.

-   **Activity** provides reports that drill down on the specific
    activity by developer, product, API, and operation.


*End of lab.*