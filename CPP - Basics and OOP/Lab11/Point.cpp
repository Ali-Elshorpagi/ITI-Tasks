//
//#include <iostream>
//
//using namespace std;
//
//class Geoshape
//{
//public:
//    virtual void print() = 0;
//};
//class Point
//{
//private:
//    int _x;
//    int _y;
//
//public:
//    Point() : Point(0, 0) { cout << "Initialize Point Constructor Called\n\n"; }
//    Point(int x) : _x(x), _y(x) {}
//    Point(int x, int y) : _x(x), _y(y) { cout << "Parameterized Point Constructor Called\n\n"; }
//    void setX(int x) { _x = x; }
//    int getX() { return _x; }
//    void setY(int y) { _y = y; }
//    int getY() { return _y; }
//    void setPoint(int x, int y) { _x = x, _y = y; }
//    void print() { cout << "( " << _x << " , " << _y << " )"; }
//    ~Point() { cout << "Destructor Point Called\n\n"; }
//};
//
//class Rectangle : public Geoshape
//{
//private:
//    Point _ul;
//    Point _lr;
//
//public:
//    Rectangle() : Rectangle(0, 0, 0, 0) { cout << "Initialize Rectangle Composition Called\n\n"; }
//    Rectangle(int x1, int y1, int x2, int y2) : _ul(x1, y1), _lr(x2, y2) { cout << "Parameterized Rectangle Composition Called\n\n"; }
//    void print()
//    {
//        cout << "Rectangle: ";
//        _ul.print();
//        _lr.print();
//        cout << '\n';
//    }
//    ~Rectangle() { cout << "Destructor Rectangle Composition Called\n\n"; }
//};
//
//class Triangle : public Geoshape
//{
//private:
//    Point a, b, c;
//
//public:
//    Triangle() { cout << "Initialize Triangle Composition Constructor Called\n\n"; }
//    Triangle(int x1, int y1, int x2, int y2, int x3, int y3) : a(x1, y1), b(x2, y2), c(x3, y3) { cout << "Parameterized Triangle Composition Called\n\n"; }
//    void print()
//    {
//        cout << "Triangle: ";
//        a.print();
//        b.print();
//        c.print();
//        cout << '\n';
//    }
//    ~Triangle() { cout << "Destructor Triangle Composition Called\n\n"; }
//};
//
//class Circle : public Geoshape
//{
//private:
//    Point center;
//    int radius;
//
//public:
//    Circle() : center(0, 0), radius(0) { cout << "Initialize Circle Composition Constructor Called\n\n"; }
//    Circle(int x1, int y1, int r) : center(x1, y1), radius(r) { cout << "Parameterized Circle Composition Called\n\n"; }
//    void print()
//    {
//        cout << "Circle: ";
//        center.print();
//        cout << " , Radius = " << radius << '\n';
//    }
//    ~Circle() { cout << "Destructor Circle Composition Called\n\n"; }
//};
//
//void myFun(Geoshape *g) { g->print(); }
//
//void testGeoShape()
//{
//    Geoshape *g[3];
//    g[0] = new Triangle(1, 2, 3, 4, 5, 6);
//    g[1] = new Rectangle(1, 2, 3, 4);
//    g[2] = new Circle(1, 2, 4);
//
//    cout << "\n\t";
//    myFun(g[0]);
//
//    cout << "\n\t";
//    myFun(g[1]);
//
//    cout << "\n\t";
//    myFun(g[2]);
//}
