// 1- Create a base class Shape with a method calcArea().
// 2- Create subclasses Rectangle and Circle, and override the calcArea() method in each.
// 3- Create an array of different shapes (both rectangles and circles).
// 4- Loop through the array and calculate the area  shapes.


class Shape {
    calcArea() { }
}

class Rectangle extends Shape {
    #width;
    #height;
    constructor(_width, _height) {
        super();
        this.Width = _width;
        this.Height = _height;
    }
    set Width(newWidth) {
        if (newWidth < 0)
            console.log("Width must be greater than 0");
        else
            this.#width = newWidth;
    }
    get Width() {
        return this.#width;
    }
    set Height(newHeight) {
        if (newHeight < 0)
            console.log("Height must be greater than 0");
        else
            this.#height = newHeight;
    }
    get Height() {
        return this.#height;
    }
    calcArea() {
        return this.#width * this.#height;
    }
}

class Circle extends Shape {
    #radius;
    constructor(_radius) {
        super();
        this.Radius = _radius;
    }
    set Radius(newRadius) {
        if (newRadius < 0)
            console.log("Radius must be greater than 0");
        else
            this.#radius = newRadius;
    }
    get Radius() {
        return this.#radius;
    }
    calcArea() {
        return Math.PI * this.#radius * this.#radius;
    }
}

const shapes = [
    new Rectangle(4, 5),
    new Circle(3),
    new Rectangle(6, 7),
    new Circle(4)
];

shapes.forEach(shape => {
    console.log(`Area: ${Math.round(shape.calcArea() * 1000) / 1000}`);
});
