let favoritesDiv = document.getElementById('favorites');

function displayFav() {
    favoritesDiv.innerHTML = ''; // clear previous products

    let favorites = localStorage.getItem('fav');
    if (favorites)
        favorites = JSON.parse(favorites);
    else
        favorites = [];

    if (favorites.length === 0) {
        favoritesDiv.innerHTML = '<p style="text-align: center; margin-top: 50px; font-size: 20px;">No favorites yet!</p>';
        return;
    }
    for (let item of favorites) {

        let productDiv = document.createElement('div');
        productDiv.classList = 'productCard';

        let img = document.createElement('img');
        img.src = item.image;
        img.alt = item.name;

        let title = document.createElement('h3');
        title.textContent = item.title;

        let description = document.createElement('p');
        description.textContent = item.description;

        let price = document.createElement('p');
        price.classList = 'price';
        price.textContent = '$' + item.price;

        let addToCartBtn = document.createElement('button');
        addToCartBtn.textContent = 'Add to Cart';
        addToCartBtn.classList = 'addToCart';

        addToCartBtn.addEventListener('click', (function (product) {
            return function () {
                let cart = localStorage.getItem('cart');
                if (cart)
                    cart = JSON.parse(cart);
                else
                    cart = [];

                let existingProduct = cart.find(function (p) {
                    return p.id === product.id;
                });

                if (existingProduct) {
                    existingProduct.quantity += 1;
                } else {
                    product.quantity = 1;
                    cart.push(product);
                }

                localStorage.setItem('cart', JSON.stringify(cart));
                alert('Added to cart!');
            };
        })(item));

        let removeBtn = document.createElement('button');
        removeBtn.textContent = 'Remove';
        removeBtn.classList = 'removeBtn';

        removeBtn.addEventListener('click', (function (product) {
            return function () {
                let fav = localStorage.getItem('fav');
                if (fav)
                    fav = JSON.parse(fav);
                else
                    fav = [];

                fav = fav.filter(function (p) {
                    return p.id !== product.id;
                });

                localStorage.setItem('fav', JSON.stringify(fav));
                displayFav();
            };
        })(item));

        productDiv.appendChild(img);
        productDiv.appendChild(title);
        productDiv.appendChild(description);
        productDiv.appendChild(price);
        productDiv.appendChild(addToCartBtn);
        productDiv.appendChild(removeBtn);

        favoritesDiv.appendChild(productDiv);
    }
}

displayFav();