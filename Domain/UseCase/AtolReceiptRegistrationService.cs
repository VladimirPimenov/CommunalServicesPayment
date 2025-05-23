﻿using CommunalServices.Domain.Contracts;
using CommunalServices.Domain.Repositories;
using CommunalServices.Domain.Entities;

namespace CommunalServices.Domain.UseCase
{
    public class AtolReceiptRegistrationService(
        IConfiguration config,
        IReceiptRegistrationRepository receiptRepository) : IReceiptRegistrationService
    {
        private readonly string atolServiceLink = config.GetValue<string>("AtolServiceLink");

        public async Task<bool> RegisterReceiptAsync(int abonentId, int paymentAccountId)
        {
            var abonent = await receiptRepository.GetAbonentByIdAsync(abonentId);
            var paymentAccount = await receiptRepository.GetPaymentAccountByIdAsync(paymentAccountId);

            if (abonent == null || paymentAccount == null)
                return false;

            var json = CreateJsonReceipt(abonent, paymentAccount);

            bool receiptResult = await SendJsonReceiptToAtol(json);
            return receiptResult;
        }

        private JsonContent CreateJsonReceipt(Abonent abonent, PaymentAccount paymentAccount)
        {
            var mockRequest = new { abonent, paymentAccount };

            var json = JsonContent.Create(mockRequest);

            return json;
        }

        private async Task<bool> SendJsonReceiptToAtol(JsonContent receipt)
        {
            HttpClient httpClient = new HttpClient();

            var httpResponce = await httpClient.PostAsync(atolServiceLink, receipt);

            return httpResponce.IsSuccessStatusCode; 
        }
    }
}
