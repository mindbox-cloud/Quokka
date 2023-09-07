// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests;

[TestClass]
public sealed class RenderSettingsCultureInfoTests
{
    [TestMethod]
    [DataRow("en-US")]
    [DataRow("ru-RU")]
    public void FormatDateTimeTemplateFunction_ReturnsLocalizedResult(string locale)
    {
        var dt = DateTime.UtcNow;
        var settings = new RenderSettings { CultureInfo = new CultureInfo(locale) };
        var expectedFormat = dt.ToString("MMMM", settings.CultureInfo);
        
        var template = new Template("${FormatDateTime(Date, 'MMMM')}");
        var result = template.Render(
            new CompositeModelValue(new ModelField("Date", dt)),
            settings);
        
        Assert.AreEqual(expectedFormat, result);
    }
    
    [TestMethod]
    [DataRow("en-US")]
    [DataRow("ru-RU")]
    public void FormatTimeTemplateFunction_ReturnsLocalizedResult(string locale)
    {
        var dt = DateTime.UtcNow.TimeOfDay;
        var settings = new RenderSettings { CultureInfo = new CultureInfo(locale) };
        var expectedFormat = dt.ToString("g", settings.CultureInfo);
        
        var template = new Template("${FormatTime(Date, 'g')}");
        var result = template.Render(
            new CompositeModelValue(new ModelField("Date", dt)),
            settings);
        
        Assert.AreEqual(expectedFormat, result);
    }
    
    [TestMethod]
    [DataRow("Карл у Клары украл кораллы", "ru-RU")]
    [DataRow("Clara stole Carl's Clarinet", "en-US")]
    public void ToUpperTemplateFunction_ReturnsLocalizedResult(string input, string locale)
    {
        var settings = new RenderSettings { CultureInfo = new CultureInfo(locale) };
        var expectedFormat = input.ToUpper(settings.CultureInfo);
        
        var template = new Template("${ToUpper(Input)}");
        var result = template.Render(
            new CompositeModelValue(new ModelField("Input", input)),
            settings);
        
        Assert.AreEqual(expectedFormat, result);
    }
    
    [TestMethod]
    [DataRow("Карл у Клары украл кораллы", "ru-RU")]
    [DataRow("Clara stole Carl's Clarinet", "en-US")]
    public void ToLowerTemplateFunction_ReturnsLocalizedResult(string input, string locale)
    {
        var settings = new RenderSettings { CultureInfo = new CultureInfo(locale) };
        var expectedFormat = input.ToLower(settings.CultureInfo);
        
        var template = new Template("${ToLower(Input)}");
        var result = template.Render(
            new CompositeModelValue(new ModelField("Input", input)),
            settings);
        
        Assert.AreEqual(expectedFormat, result);
    }
    
    [TestMethod]
    [DataRow("en-US")]
    [DataRow("ru-RU")]
    public void FormatDecimalTemplateFunction_ReturnsLocalizedResult(string locale)
    {
        var input = (decimal)Random.Shared.NextDouble() * 1000;
        var settings = new RenderSettings { CultureInfo = new CultureInfo(locale) };
        var expectedFormat = input.ToString("C", settings.CultureInfo);
        
        var template = new Template("${FormatDecimal(Input, 'C')}");
        var result = template.Render(
            new CompositeModelValue(new ModelField("Input", input)),
            settings);
        
        Assert.AreEqual(expectedFormat, result);
    }
    
    [TestMethod]
    [DataRow("Карл у Клары украл кораллы", "ru-RU")]
    [DataRow("Clara stole Carl's Clarinet", "en-US")]
    public void CapitalizeAllWordsTemplateFunction_ReturnsLocalizedResult(string input, string locale)
    {
        var settings = new RenderSettings { CultureInfo = new CultureInfo(locale) };
        var expectedFormat = settings.CultureInfo.TextInfo.ToTitleCase(input);
        
        var template = new Template("${CapitalizeAllWords(Input)}");
        var result = template.Render(
            new CompositeModelValue(new ModelField("Input", input)),
            settings);
        
        Assert.AreEqual(expectedFormat, result);
    }
}