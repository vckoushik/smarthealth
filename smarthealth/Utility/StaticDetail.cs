﻿namespace smarthealth.Utility
{
    public class StaticDetail
    {
        public static string GeminiAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "PATIENT";
        public const string TokenCookie = "DOCTOR";
        public const string Query = "Accurately identify the disease, treatments to be taken for the symptoms, Department of Doctor to choose, Medication that can be taken and Prevention measures.Input provides is symptoms query, parameters like age, gender, isAlcholic, isSmoker, height, weight, severity, city, country. Give me an output json that fits the model whose attributes are Disease is type single string,Treatments is type list string,HomeRemedies is type list string,DoctorDepartment is type single string,Medications is type list string,PreventionMeasures is type list string. Dont change the response format.Fill null if query provided is not valid symptoms."; 
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }

        public enum Status
        {
            Pending,
            Approved,
            Cancelled,
            Completed,
        }
    }
}
