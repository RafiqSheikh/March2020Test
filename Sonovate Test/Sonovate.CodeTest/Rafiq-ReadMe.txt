 
Rafiq Sheikh:

Note: Unable to use dependency injection as the class BacsExportService was created manually from Application.cs
I am not allowed to make any changes there.
       
// Folder structures:
 1. All services are under Services folder
 2. All builders are under Builders folder
 3. Unit Tests project added in the solution uner the Tests folder

// Two payment services : Interface - IPaymentService
    1.  AgencyPaymentService
        a. AgencyService - to get agencies
        b. AgencyPaymentBuilder - to build payment details
        c. Existing PaymentsRepository - to get payments (not allowed to to modify)
        this can not be mocked as there is no interface implemention and the class is internal

    2. SupplierPaymentService:
    a. SupplierPaymentBuilder - to build payment details
    b. InvoiceTransactionRepository - to get InvoiceTransaction ( not allowed to modify)

    3. PaymentServiceFactory:
    - Input : BacsExportType 
    - this factory is used to choose the payment service

    4. SavePaymentService- Two save functions have been replaced with this service
               
    5. Replaced SupplierBacsExport & BacsResult models with PaymentDetail

    6. Moved hardcoded DocumentStore Url and Database to appsettings.json
              
    7. Added Unit tests (use MS Test)- Sonovate.CodeTest.UnitTests under the Tests folder.

Further Improvements:
1. Appsettings: EnableAgencyPayments (Application.cs) needs to be moved to appsettings
2. Dependency Injection
3. Add interfaces to all repository CandidateRepository, InvoiceTransactionRepository and PaymentsRepository
    Inject them
4. Add logs