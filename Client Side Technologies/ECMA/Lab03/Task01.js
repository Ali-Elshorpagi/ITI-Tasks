const products = [
    { id: 1, name: "Laptop", price: 30000, stock: 5 },
    { id: 2, name: "Phone", price: 15000, stock: 0 },
    { id: 3, name: "Headphone", price: 2000, stock: 10 },
    { id: 4, name: "Keyboard", price: 1000, stock: 3 }
];

function getProducts() {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve(products);
        }, 500);
    });
}

getProducts()
    .then((products) => {
        return products.filter(product => product.stock > 0);
    })
    .then((filteredProducts) => {
        let totalPrice = filteredProducts.reduce((total, product) => total + product.price, 0);
        return totalPrice;
    })
    .then((totalPrice) => {
        return new Promise((resolve, reject) => {
            if (totalPrice < 5000)
                reject("Total price is less than 5000, discount cannot be applied.");

            resolve(totalPrice * 0.1);
        });
    })
    .then((finalPrice) => {
        console.log(`Final price after discount: ${finalPrice}`);
    })
    .catch((error) => {
        console.error(error);
    });
