// Part One:
// 1- Use a constructor function to implement A Car .
// A Car has a Name and a Speed property. The Speed property is the Current Speed of the Car in Km/h

export function Car(_name, _speed) {
    this.name = _name;
    this.speed = _speed;
}

// Using Prototype to:
// 2- Implement an ‘accelerate’ method will increase the car’s speed by 10, and log then new speed to console;
Car.prototype.accelerate = function () {
    this.speed += 10;
    console.log(`${this.name} is going at ${this.speed} km/h`);
}

// 3- Implement a ‘brake’ method that will decrease the car’s speed by 5,and log the new speed to the console;
Car.prototype.brake = function () {
    this.speed -= 5;
    console.log(`${this.name} is going at ${this.speed} km/h`);
}

// 4- Create 2 car objects and experiment with calling ‘accelerate’ and ‘brake’ multiple times on each of them.
const car1 = new Car('BMW', 120);
const car2 = new Car('Mercedes', 95);

car1.accelerate(); // BMW is going at 130 km/h
car1.brake(); // BMW is going at 125 km/h
car2.accelerate(); // Mercedes is going at 105 km/h
car2.brake();// Mercedes is going at 100 km/h