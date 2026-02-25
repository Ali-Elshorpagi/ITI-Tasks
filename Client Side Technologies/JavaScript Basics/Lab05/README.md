# Lab05

## Overview

A **multi-page e-commerce web application** called **ElectroShop** demonstrating **DOM manipulation**, **`localStorage`** for persistent data, **product search**, **Add to Cart** with quantity management, and **Add to Favorites**. The app dynamically creates product cards from a JavaScript array and uses `localStorage` to persist cart and favorites data across pages.

---

## Files

```
Lab05/
├── scripts/
│   ├── script.js
│   ├── cart_script.js
│   └── fav_script.js
├── styles/
│   ├── style.css
│   ├── cart_style.css
│   └── fav_style.css
├── index.html
├── cart.html
└── favourit.html
```

---

## Page Details

### 1. Shop Page (`index.html` + `script.js`)

- Displays a grid of **15 product cards** generated dynamically from a `products` array.
- Each card includes: image (from Unsplash), title, description, price, **Add to Cart** button, and **❤️ Favorites** button.
- **Search bar** (`keyup` event): live filtering by product name or title.
- **Add to Cart**: stores product in `localStorage` under the key `'cart'`, tracks quantity (increments if already exists).
- **Add to Favorites**: stores product in `localStorage` under the key `'fav'`, prevents duplicates.
- All elements created using `document.createElement()` and `appendChild()`.

---

### 2. Cart Page (`cart.html` + `cart_script.js`)

- Reads cart data from `localStorage.getItem('cart')`.
- Displays cart items with quantity management.
- Shows total price calculation.
- Styled with dedicated `cart_style.css`.

---

### 3. Favorites Page (`favourit.html` + `fav_script.js`)

- Reads favorites data from `localStorage.getItem('fav')`.
- Displays favorited products.
- Styled with dedicated `fav_style.css`.

---

## Site Navigation

```
ElectroShop (Shop) ←→ Favourites ←→ Cart
```

All pages share a consistent navigation header with links to Shop, Favourites, and Cart.

---

## Key Concepts Demonstrated

- **`localStorage`** — `setItem`, `getItem`, `JSON.stringify`, `JSON.parse` for persistent storage
- **Dynamic DOM creation** — `createElement`, `appendChild`, `classList`, `addEventListener`
- **Closures** — IIFE pattern `(function(product) { return function() {...} })(item)` for event handlers
- **Live search** — `keyup` event with `filter()` and `includes()`
- **Multi-page architecture** — separate HTML pages sharing data via `localStorage`
- **Cart quantity management** — increment existing items or add new entries
- **External CSS** — separate stylesheets per page

---
