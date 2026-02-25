let cartDiv = document.getElementById('cartContainer');
let totalDiv = document.getElementById('total');

function displayCart() {
    cartDiv.innerHTML = ''; // clear previous products
    let total = 0;
    let carts = localStorage.getItem('cart');
    if (carts)
        carts = JSON.parse(carts);
    else
        carts = [];

    if (carts.length === 0) {
        cartDiv.innerHTML = '<p style="text-align: center; margin-top: 50px; font-size: 20px;">No favorites yet!</p>';
        totalDiv.innerHTML = '';
        return;
    }
    for (let item of carts) {

        let productDiv = document.createElement('div');
        productDiv.classList = 'productCard';

        let img = document.createElement('img');
        img.src = item.image;
        img.alt = item.name;

        let title = document.createElement('h3');
        title.textContent = item.title;

        let description = document.createElement('p');
        description.textContent = item.description;

        let priceQuantityDiv = document.createElement('div');
        priceQuantityDiv.classList = 'priceQuantityRow';

        let price = document.createElement('span');
        price.classList = 'price';
        price.textContent = '$' + item.price + ' x';

        let quantityDiv = document.createElement('div');
        quantityDiv.classList = 'quantityControls';

        let minusBtn = document.createElement('button');
        minusBtn.textContent = '-';
        minusBtn.classList = 'quantityBtn';

        let quantityInput = document.createElement('input');
        quantityInput.type = 'number';
        quantityInput.value = item.quantity;
        quantityInput.min = '1';
        quantityInput.classList = 'quantityInput';

        let plusBtn = document.createElement('button');
        plusBtn.textContent = '+';
        plusBtn.classList = 'quantityBtn';

        plusBtn.addEventListener('click', (function (p) {
            return function () {
                let cart = localStorage.getItem('cart');
                if (cart)
                    cart = JSON.parse(cart);
                else
                    cart = [];
                for (let prod of cart) {
                    if (prod.id === p.id) {
                        prod.quantity += 1;
                        break;
                    }
                }
                localStorage.setItem('cart', JSON.stringify(cart));
                displayCart();
            };
        })(item));

        minusBtn.addEventListener('click', (function (p) {
            return function () {
                let cart = localStorage.getItem('cart');
                if (cart)
                    cart = JSON.parse(cart);
                else
                    cart = [];
                for (let prod of cart) {
                    if (prod.id === p.id && prod.quantity > 1) {
                        prod.quantity -= 1;
                        break;
                    }
                }
                localStorage.setItem('cart', JSON.stringify(cart));
                displayCart();
            };
        })(item));

        let removeBtn = document.createElement('button');
        removeBtn.textContent = 'Remove';
        removeBtn.classList = 'removeBtn';

        removeBtn.addEventListener('click', (function (product) {
            return function () {
                let cart = localStorage.getItem('cart');
                if (cart)
                    cart = JSON.parse(cart);
                else
                    cart = [];

                cart = cart.filter(function (p) {
                    return p.id !== product.id;
                });

                localStorage.setItem('cart', JSON.stringify(cart));
                displayCart();
            };
        })(item));

        let subtotalDiv = document.createElement('p');
        let subPrice = item.price * item.quantity;
        subtotalDiv.classList = 'subtotal';
        subtotalDiv.textContent = `Subtotal: $${subPrice}`;

        productDiv.appendChild(img);
        productDiv.appendChild(title);
        productDiv.appendChild(description);

        priceQuantityDiv.appendChild(price);
        quantityDiv.appendChild(minusBtn);
        quantityDiv.appendChild(quantityInput);
        quantityDiv.appendChild(plusBtn);
        priceQuantityDiv.appendChild(quantityDiv);

        productDiv.appendChild(priceQuantityDiv);
        productDiv.appendChild(subtotalDiv);
        productDiv.appendChild(removeBtn);

        cartDiv.appendChild(productDiv);

        total += subPrice;
    }
    totalDiv.innerHTML = '<p class="totalPrice">Total: $' + total + '</p>';
}

displayCart();