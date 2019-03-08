//  ------------------------------------------------------------------------------------------------
//   <copyright file="TestRequest.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Tests.Infrastructure
{
    using MediatR;

    public class TestRequest : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"{{{nameof(Id)} = {Id}, {nameof(Name)} = {Name}}}";

    }
}