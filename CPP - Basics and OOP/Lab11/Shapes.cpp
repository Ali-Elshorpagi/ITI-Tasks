#include <iostream>

using namespace std;

class Shape
{
protected:
    int _dim1;
    int _dim2;

public:
    Shape(int dim1, int dim2) : _dim1(dim1), _dim2(dim2) {}
    void setDim1(int dim1) { _dim1 = dim1; }
    int getDim1() { return _dim1; }
    void setDim2(int dim2) { _dim2 = dim2; }
    int getDim2() { return _dim2; }
    void setDim(int dim1, int dim2) { _dim1 = dim1, _dim2 = dim2; }
    virtual double getArea() = 0;
    virtual ~Shape() {}
};

class Rectangle : public Shape
{
public:
    Rectangle(int _dim1, int _dim2) : Shape(_dim1, _dim2) {}
    double getArea() { return _dim1 * _dim2; }
};

class Square : public Rectangle
{
public:
    Square(int _dim) : Rectangle(_dim, _dim) {}
    double getArea() { return _dim1 * _dim1; }
};
class Circle : public Shape
{
private:
    const double pi{3.14};

public:
    Circle(int radius) : Shape(radius, radius) {}
    double getArea() { return pi * _dim1 * _dim2; }
};
class Triangle : public Shape
{
public:
    Triangle(int dim1, int dim2) : Shape(dim1, dim2) {}
    double getArea() override { return 0.5 * _dim1 * _dim2; }
};

double myFun(Shape *s) { return s->getArea(); }

void shapesTest()
{
    const int MAX_SHAPES(10);
    int choice;
    Shape *s[MAX_SHAPES];
    int cnt(-1);
    do
    {
        system("cls");
        cout << "\n\t>> MAIN MENU <<"
             << "\n\t[1]. Area Of Rectangle"
             << "\n\t[2]. Area Of Square"
             << "\n\t[3]. Area Of Triangle"
             << "\n\t[4]. Area Of Circle"
             << "\n\t[5]. Sum Area of Shapes"
             << "\n\t[6]. Exit\n"
             << "\n\n\tEnter Choice: ";
        cin >> choice;

        switch (choice)
        {
        case 1:
        {
            if (cnt < MAX_SHAPES - 1)
            {
                int w, h;
                cout << "\t\tEnter Width and Height: ";
                cin >> w >> h;
                s[++cnt] = new Rectangle(w, h);
                cout << "\n\tRectangle Area = " << myFun(s[cnt]) << "\n\n";
                system("pause");
            }
            else
                cout << "\t\tShapes is Full\n\n";

            break;
        }
        case 2:
        {
            if (cnt < MAX_SHAPES - 1)
            {
                int dim;
                cout << "\t\tEnter Side: ";
                cin >> dim;
                s[++cnt] = new Square(dim);
                cout << "\n\tSquare Area = " << myFun(s[cnt]) << "\n\n";
                system("pause");
            }
            else
                cout << "\t\tShapes is Full\n\n";
            break;
        }
        case 3:
        {
            if (cnt < MAX_SHAPES - 1)
            {
                int base, height;
                cout << "\t\tEnter Base And Height: ";
                cin >> base >> height;
                s[++cnt] = new Triangle(base, height);
                cout << "\n\tTriangle Area = " << myFun(s[cnt]) << "\n\n";
                system("pause");
            }
            else
                cout << "\t\tShapes is Full\n\n";
            break;
        }
        case 4:
        {
            if (cnt < MAX_SHAPES - 1)
            {
                int radius;
                cout << "\t\tEnter Radius: ";
                cin >> radius;
                s[++cnt] = new Circle(radius);
                cout << "\n\tCircle Area = " << myFun(s[cnt]) << "\n\n";
                system("pause");
            }
            else
                cout << "\t\tShapes is Full\n\n";
            break;
        }
        case 5:
        {
            double sum(0.0);
            for (int i(0); i <= cnt; ++i)
                sum += s[i]->getArea();
            cout << "\n\t\tSum Of Areas = " << sum << "\n\n";
            system("pause");
            break;
        }
        case 6:
            cout << "\nExit the application ...\n";
            break;
        default:
            cout << "Invalid Input, plz enter a number between 1 to 6\n";
            break;
        }
    } while (choice != 6);
}
