#include "circular_queue.cpp"
#include "stack_linkedListBased.cpp"
#include "queue_linkedListBased.cpp"
#include "expression.cpp"

void Test_circular_queue()
{
    Circular_Queue<int> q(5);

    std::cout << "Testing Circular Queue" << edl << edl;

    std::cout << "Enqueuing 10, 20, 30:" << edl;
    q.enqueue(10), q.enqueue(20), q.enqueue(30);
    q.print();

    std::cout << "Dequeuing: " << q.dequeue() << edl;
    q.print();

    std::cout << "Enqueuing 40, 50, 60:" << edl;
    q.enqueue(40), q.enqueue(50), q.enqueue(60);
    q.print();

    std::cout << "Is full? " << (q.is_full() ? "Yes" : "No") << edl << edl;

    // Test insert_after and insert_before
    std::cout << "Testing insert_after and insert_before:" << edl;
    Circular_Queue<int> q2(7);
    q2.enqueue(1), q2.enqueue(2), q2.enqueue(3);
    q2.print();
    q2.insert_after(2, 22); // after 2
    q2.print();
    q2.insert_before(3, 33); // before 3
    q2.print();
    q2.insert_before(1, 11); // before 1 (front)
    q2.print();
    q2.insert_after(100, 200); // not found
    q2.print();
    while (!q2.is_empty())
        q2.dequeue();
    q2.print();

    std::cout << "Dequeuing all elements:" << edl;
    while (!q.is_empty())
        std::cout << q.dequeue() << " ";

    std::cout << edl << edl;
    std::cout << "Is empty? " << (q.is_empty() ? "Yes" : "No") << edl;
    q.print();
}

void Test_stack()
{
    Stack_LinkedList_Based<int> s;

    std::cout << "Testing Stack (Linked List Based)" << edl << edl;

    std::cout << "Pushing 5, 10, 15, 20:" << edl;
    s.push(5), s.push(10), s.push(15), s.push(20);
    s.print();

    std::cout << "Peek top: " << s.peek() << edl
              << "Stack size: " << s.get_size() << edl << edl;

    std::cout << "Popping: " << s.pop() << edl
              << "After pop:" << edl;
    s.print();

    std::cout << "Pushing 25, 30:" << edl;
    s.push(25), s.push(30);
    s.print();

    // Test insert_after and insert_before
    std::cout << "Testing insert_after and insert_before:" << edl;
    Stack_LinkedList_Based<int> s2;
    s2.push(10), s2.push(20), s2.push(30);
    s2.print();
    s2.insert_after(20, 25); // after 20
    s2.print();
    s2.insert_before(10, 5); // before 10
    s2.print();
    s2.insert_before(30, 15); // before 30 (head)
    s2.print();
    s2.insert_after(100, 200); // not found
    s2.print();
    while (!s2.is_empty())
        s2.pop();
    s2.print();

    std::cout << "Popping all elements:" << edl;
    while (!s.is_empty())
        std::cout << s.pop() << " ";

    std::cout << edl << edl;
    std::cout << "Is empty? " << (s.is_empty() ? "Yes" : "No") << edl << edl;
}

void Test_queue()
{
    Queue_LinkedList_Based<int> q;

    std::cout << "Testing Queue (Linked List Based)" << edl << edl;

    std::cout << "Enqueuing 100, 200, 300, 400:" << edl;
    q.enqueue(100), q.enqueue(200), q.enqueue(300), q.enqueue(400);
    q.print();

    std::cout << "Queue size: " << q.get_size() << edl << edl
              << "Dequeuing: " << q.dequeue() << edl
              << "After dequeue:" << edl;
    q.print();

    std::cout << "Enqueuing 500, 600:" << edl;
    q.enqueue(500), q.enqueue(600);
    q.print();

    // Test insert_after and insert_before
    std::cout << "Testing insert_after and insert_before:" << edl;
    Queue_LinkedList_Based<int> q2;
    q2.enqueue(1), q2.enqueue(2), q2.enqueue(3);
    q2.print();
    q2.insert_after(2, 22); // after 2
    q2.print();
    q2.insert_before(3, 33); // before 3
    q2.print();
    q2.insert_before(1, 11); // before 1 (head)
    q2.print();
    q2.insert_after(100, 200); // not found
    q2.print();
    while (!q2.is_empty())
        q2.dequeue();
    q2.print();

    std::cout << "Dequeuing all elements:" << edl;
    while (!q.is_empty())
        std::cout << q.dequeue() << " ";

    std::cout << edl << edl;
    std::cout << "Is empty? " << (q.is_empty() ? "Yes" : "No") << edl << edl;
}

void Test_expression()
{
    Expression expr;

    std::cout << "Testing Expression (Infix to Postfix & Evaluation)" << edl << edl;

    std::string infix1("3+4*2");
    std::cout << "Infix:   " << infix1 << edl;
    int result1(expr.eval(infix1));
    std::cout << "Result:  " << result1 << edl << edl;

    std::string infix2("123+456");
    std::cout << "Infix:   " << infix2 << edl;
    int result2(expr.eval(infix2));
    std::cout << "Result:  " << result2 << edl << edl;

    std::string infix3("(100+200)*3");
    std::cout << "Infix:   " << infix3 << edl;
    int result3(expr.eval(infix3));
    std::cout << "Result:  " << result3 << edl << edl;

    std::string infix4("45-18/2+7*3");
    std::cout << "Infix:   " << infix4 << edl;
    int result4(expr.eval(infix4));
    std::cout << "Result:  " << result4 << edl << edl;

    std::string infix5("((15+25)*2-10)/5");
    std::cout << "Infix:   " << infix5 << edl;
    int result5(expr.eval(infix5));
    std::cout << "Result:  " << result5 << edl << edl;

    std::cout << "--- Testing Error Handling ---" << edl << edl;

    std::string infix6("10/0");
    std::cout << "Infix:   " << infix6 << edl;
    int result6(expr.eval(infix6));
    std::cout << "Result:  " << result6 << edl << edl;

    std::string infix7("(5+3)/(2-2)");
    std::cout << "Infix:   " << infix7 << edl;
    int result7(expr.eval(infix7));
    std::cout << "Result:  " << result7 << edl << edl;

    std::string infix8("");
    std::cout << "Infix:   (empty)" << edl;
    int result8(expr.eval(infix8));
    std::cout << "Result:  " << result8 << edl << edl;
}

int main()
{
    Test_stack();
    Test_queue();
    Test_circular_queue();
    // Test_expression();

    return (0);
}