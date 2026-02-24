# Lab03

## Overview

A **multi-page e-commerce-style website** demonstrating the use of **external CSS stylesheets**, **semantic HTML elements** (`<header>`, `<nav>`, `<footer>`), **navigation between pages**, and **form-based authentication** (Login & Sign Up). The site includes a product catalog, about page, contact page, and individual product detail page.

---

## Files

```
Lab03/
├── assets/
│   ├── Creativa.png
│   ├── ITI.png
│   ├── MM.png
│   ├── bk.png
│   ├── download.jpg
│   ├── h1.png
│   ├── h2.jpg
│   ├── home.png
│   ├── l1.jpg
│   ├── l2.jpg
│   ├── l3.jpg
│   ├── mail.png
│   ├── pass.png
│   └── passs.png
├── styles/
│   ├── aboutus_style.css
│   ├── contac_style.css
│   ├── login_style.css
│   ├── product_style.css
│   ├── products_style.css
│   └── signup_style.css
├── task/
│   ├── Images/
│   └── Lab.webm
├── aboutus.html
├── contact.html
├── login.html
├── product.html
├── products.html
└── signup.html
```

---

## Page Details

### 1. Products Page (`products.html`)

- A **product catalog** displaying a grid of items (laptops and headphones) using a table layout.
- Each product image links to the product detail page (`product.html`).
- Product labels: HP Laptop, Mac Book, Sony Head Phone.
- Includes a shared **header** with logo and navigation, and a **footer** with copyright.
- Styled via `products_style.css`.

---

### 2. About Us Page (`aboutus.html`)

- Contains a **header with navigation** bar (Products, About Us, Contact Us).
- A content section with placeholder text about ITI.
- A **logo table** displaying three partner logos: ITI, Creativa, MM.
- Footer with copyright text.
- Styled via `aboutus_style.css`.

---

### 3. Contact Us Page (`contact.html`)

- Same navigation header as other pages.
- A content section with contact information.
- A **contact details table** with three columns:
  - ☎️ **Call Us** — phone numbers with `tel:` links
  - 🗺️ **Location** — physical address
  - 🕛 **Hours** — business hours
- Footer with copyright text.
- Styled via `contac_style.css`.

---

### 4. Login Page (`login.html`)

- A centered **login form** with:
  - User icon image + Username text input.
  - Password icon image + Password input.
  - **Login** submit button.
  - Link to the **Sign Up** page.
- Form submits via `POST` to `products.html`.
- Styled via `login_style.css`.

---

### 5. Sign Up Page (`signup.html`)

- A centered **registration form** with:
  - User icon + Username input.
  - Mail icon + Email input.
  - Password icon + Password input.
  - Password icon + Confirm Password input.
  - **Register** submit button.
  - Link to the **Login** page.
- Form submits via `POST` to `products.html`.
- Styled via `signup_style.css`.

---

### 6. Product Detail Page (`product.html`)

- Shows a **single product** with:
  - Product image (HP Laptop).
  - Product name heading.
  - Specifications: Processor, Storage, RAM, Price.
  - A **WhatsApp contact link** with icon for purchasing.
- Same navigation header as other pages.
- Styled via `product_style.css`.

---

## Site Navigation

```
Login ←→ Sign Up
  ↓          ↓
Products Page
  ├── About Us
  ├── Contact Us
  └── Product Detail
```

---

## Key Concepts Demonstrated

- **External CSS** — separate `.css` files linked via `<link rel="stylesheet">`
- **Semantic HTML** — `<header>`, `<nav>`, `<footer>` for page structure
- **Navigation bar** — consistent `<nav>` with `<ul>`/`<li>` links across pages
- **Form-based auth** — Login and Sign Up forms with `POST` method
- **Image assets** — product images, icons, and partner logos
- **Multi-page architecture** — interconnected pages with relative hyperlinks
- **Table-based product grid** — product catalog layout using `<table>`
- **WhatsApp deep link** — `wa.me` integration for product inquiries

---
