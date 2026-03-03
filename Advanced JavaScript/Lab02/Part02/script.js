// Part Two:
// 1- Use A constructor function to implement an Electric Car (Called EV) as a CHILD “class” 
// of Car Besides a Name and Current Speed ,the EV also has the Current battery charge in % (‘charge’ property );

import { Car } from "../Part 01/script.js";

function ElectricCar(_name, _speed, _charge) {
    Car.call(this, _name, _speed);
    this.charge = _charge;
}

ElectricCar.prototype = Object.create(Car.prototype);
ElectricCar.prototype.constructor = ElectricCar;

// 2- Implement a ‘chargeBattery’ method which takes an arguments ‘chargeTo’ and sets the battery charge to this value;
ElectricCar.prototype.chargeBattery = function (_chargeTo) {
    this.charge = _chargeTo;
};

// 3- Implment an ‘accelerate’ method that will increase the car’s speed by 20, and decrease the charge by 1% ,then log a message like this :
// ‘Tesla going at 149 km/h, with a charge of 22%’;
ElectricCar.prototype.accelerate = function () {
    this.speed += 20;
    this.charge -= 1;
    console.log(`${this.name} going at ${this.speed} km/h, with a charge of ${this.charge}%`);
};

// 4- Create an electric car object and experiment with calling ‘acceleracte ‘,’brake’ and
// ‘chargeBattery’
const ev1 = new ElectricCar('Tesla', 120, 23);
ev1.chargeBattery(90);
ev1.accelerate();
ev1.brake();
ev1.accelerate();
console.log(ev1.charge);
// (charge to 90%). Notice what happens when you ‘ accelerate
// DATA CAR 1 :’ Tesla’ going at 120 km/h , with a charge of 23%
