function displayCountryData(countryData, prefix = "") {
    const containerId = prefix === "neighbor" ? "neighbors" : "countryInfo";
    const nameId = prefix ? `${prefix}Name` : "countryName";
    const capitalId = prefix ? `${prefix}Capital` : "capital";
    const regionId = prefix ? `${prefix}Region` : "region";
    const populationId = prefix ? `${prefix}Population` : "population";
    const flagId = prefix ? `${prefix}Flag` : "flag";
    const currencyId = prefix ? `${prefix}Currency` : "currency";

    document.getElementById(containerId).style.display = "block";
    document.getElementById(nameId).innerHTML = countryData.name;
    document.getElementById(capitalId).innerHTML = countryData.capital;
    document.getElementById(regionId).innerHTML = countryData.region;
    document.getElementById(populationId).innerHTML = countryData.population;
    document.getElementById(flagId).src = countryData.flag;

    const currencies = countryData.currencies;
    if (currencies && currencies.length > 0) {
        document.getElementById(currencyId).innerHTML = `${currencies[0].name} (${currencies[0].code})`;
    } else {
        document.getElementById(currencyId).innerHTML = 'N/A';
    }
}

function fetchCountryData(url, prefix = "") {
    return fetch(url)
        .then((response) => response.json())
        .then((data) => {
            const countryData = Array.isArray(data) ? data[0] : data;
            displayCountryData(countryData, prefix);
            return countryData;
        })
        .catch((error) => {
            console.log(`Error fetching ${prefix}country:`, error);
            throw error;
        });
}

window.addEventListener("DOMContentLoaded", function () {
    let countryInput = document.getElementById("SearchCountry");
    let btn = document.getElementById("btnData");

    btn.addEventListener("click", function () {
        let countryname = countryInput.value;
        let countryUrl = `https://restcountries.com/v2/name/${countryname}`;

        fetchCountryData(countryUrl)
            .then((countryData) => {
                console.log(countryData);

                const borders = countryData.borders;
                if (borders && borders.length > 0) {
                    let neighborUrl = `https://restcountries.com/v2/alpha/${borders[1]}`;
                    fetchCountryData(neighborUrl, "neighbor");
                } else {
                    const neighborsContainer = document.getElementById("neighbors");
                    neighborsContainer.style.display = "block";
                    neighborsContainer.innerHTML = "<p>No neighboring countries</p>";
                }
            })
            .catch((error) => {
                console.log("Error:", error);
            });
    });
});