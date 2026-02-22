#include <iostream>

using namespace std;

class Point
{
    int _x;
    int _y;

public:
    Point(int x, int y)
    {
        cout << "\nConstructor of Point was called\n";
        this->_x = x;
        this->_y = y;
    }
    void setX(int x)
    {
        if (x > 0)
            this->_x = x;
    }
    void setY(int y)
    {
        if (y > 0)
            this->_y = y;
    }
    void setXY(int x, int y)
    {
        if (x > 0)
            this->_x = x;
        if (y > 0)
            this->_y = _y;
    }
    int getX()
    {
        return this->_x;
    }
    int getY()
    {
        return this->_y;
    }
    ~Point()
    {
        cout << "\nDestructor of Point was called\n";
    }
};

class RectangleComposition
{
    Point _ul;
    Point _lr;

public:
    RectangleComposition(int x1, int y1, int x2, int y2) : _ul(x1, y1), _lr(x2, y2)
    {
        cout << "\nConstructor of RectangleComposition was called\n";
    }
    void print()
    {
        cout << "Upper Left: (" << _ul.getX() << ", " << _ul.getY() << ")\n"
             << "Lower Right: (" << _lr.getX() << ", " << _lr.getY() << ")\n";
    }
    void setUL(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_ul.setX(p1.getX()), this->_ul.setY(p1.getY());
    }
    void setLR(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_lr.setX(p1.getX()), this->_lr.setX(p1.getY());
    }
    ~RectangleComposition()
    {
        cout << "\nDestructor of RectangleComposition was called\n";
    }
};

class RectangleAggregation
{
    Point *_ul;
    Point *_lr;

public:
    RectangleAggregation(Point *ul, Point *lr) : _ul(ul), _lr(lr)
    {
        cout << "\nConstructor of RectangleAggregation was called\n";
    }
    void print()
    {
        cout << "Upper Left: (" << _ul->getX() << ", " << _ul->getY() << ")\n"
             << "Lower Right: (" << _lr->getX() << ", " << _lr->getY() << ")\n";
    }
    void setUL(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_ul->setX(p1.getX()), this->_ul->setY(p1.getY());
    }
    void setLR(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_lr->setX(p1.getX()), this->_lr->setX(p1.getY());
    }
    ~RectangleAggregation()
    {
        cout << "\nDestructor of RectangleAggregation was called\n";
    }
};

class TriangleComposition
{
    Point _ul;
    Point _lr;
    Point _ut;

public:
    TriangleComposition(int x1, int y1, int x2, int y2, int x3, int y3) : _ul(x1, y1), _lr(x2, y2), _ut(x3, y3)
    {
        cout << "\nConstructor of TriangleComposition was called\n";
    }
    void print()
    {
        cout << "Upper Left: (" << _ul.getX() << ", " << _ul.getY() << ")\n"
             << "Lower Right: (" << _lr.getX() << ", " << _lr.getY() << ")\n"
             << "Upper Top: (" << _ut.getX() << ", " << _ut.getY() << ")\n";
    }
    void setUL(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_ul.setX(p1.getX()), this->_ul.setY(p1.getY());
    }
    void setLR(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_lr.setX(p1.getX()), this->_lr.setX(p1.getY());
    }
    void setUT(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_ut.setX(p1.getX()), this->_ut.setX(p1.getY());
    }
    ~TriangleComposition()
    {
        cout << "\nDestructor of TriangleComposition was called\n";
    }
};

class TriangleAggregation
{
    Point *_ul;
    Point *_lr;
    Point *_ut;

public:
    TriangleAggregation(Point *ul, Point *lr, Point *ut) : _ul(ul), _lr(lr), _ut(ut)
    {
        cout << "\nConstructor of TriangleAggregation was called\n";
    }
    void print()
    {
        cout << "Upper Left: (" << _ul->getX() << ", " << _ul->getY() << ")\n"
             << "Lower Right: (" << _lr->getX() << ", " << _lr->getY() << ")\n"
             << "Upper Top: (" << _ut->getX() << ", " << _ut->getY() << ")\n";
    }
    void setUL(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_ul->setX(p1.getX()), this->_ul->setY(p1.getY());
    }
    void setLR(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_lr->setX(p1.getX()), this->_lr->setX(p1.getY());
    }
    void setUT(Point p1)
    {
        if (p1.getX() > 0 && p1.getY() > 0)
            this->_ut->setX(p1.getX()), this->_ut->setX(p1.getY());
    }
    ~TriangleAggregation()
    {
        cout << "\nDestructor of TriangleAggregation was called\n";
    }
};

void testRectangleComposition()
{
    RectangleComposition rc(10, 10, 30, 30);
    rc.print();
    Point p1(20, 20), p2(50, 50);
    rc.setUL(p1), rc.setLR(p2);
    rc.print();
}

void testRectangleAggregation()
{
    Point p1(20, 20), p2(50, 50);
    RectangleAggregation ra(&p1, &p2);
    ra.print();
    ra.setUL(p1), ra.setLR(p2);
    ra.print();
}

void testTriangleComposition()
{
    TriangleComposition tc(10, 10, 30, 30, 20, 60);
    tc.print();
    Point p1(20, 20), p2(50, 50), p3(40, 90);
    tc.setUL(p1), tc.setLR(p2), tc.setUT(p3);
    tc.print();
}

void testTriangleAggregation()
{
    Point p1(20, 20), p2(50, 50), p3(40, 90);
    TriangleAggregation ta(&p1, &p2, &p3);
    ta.print();
    ta.setUL(p1), ta.setLR(p2), ta.setUT(p3);
    ta.print();
}

void TEST();

int main()
{
    cout << "\n\n  >> testRectangleComposition <<  \n\n";
    testRectangleComposition();
    cout << "\n------------------------------------------------------------\n";
    cout << "\n\n  >> testRectangleAggregation <<  \n\n";
    testRectangleAggregation();
    cout << "\n------------------------------------------------------------\n";
    cout << "\n\n  >> testTriangleComposition <<  \n\n";
    testTriangleComposition();
    cout << "\n------------------------------------------------------------\n";
    cout << "\n\n  >> testTriangleAggregation <<  \n\n";
    testTriangleAggregation();
    // cout << "\n------------------------------------------------------------\n";
    // cout << "\n\n  >> TEST <<  \n\n";
    // TEST();
    return 0;
}
