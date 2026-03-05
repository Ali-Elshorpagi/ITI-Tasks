#ifndef QUEUE_LINKEDLISTBASED
#define QUEUE_LINKEDLISTBASED

#include "stack_linkedListBased.cpp"

template <class type>
class Queue_LinkedList_Based
{
    Node<type> *head{nullptr};
    Node<type> *tail{nullptr};
    int size{};

public:
    Queue_LinkedList_Based() {}
    ~Queue_LinkedList_Based()
    {
        while (!is_empty())
            dequeue();
        head = tail = nullptr;
    }
    const int get_size() { return size; }
    bool is_empty() { return !head; }
    void enqueue(type val)
    {
        Node<type> *item(new Node<type>(val));
        if (is_empty())
            head = tail = item;
        else
            tail->next = item, tail = item;
        ++size;
    }
    bool insert_after(const type &target, const type &val)
    {
        for (Node<type> *cur(head); cur; cur = cur->next)
        {
            if (cur->data == target)
            {
                Node<type> *item(new Node<type>(val));
                item->next = cur->next;
                cur->next = item;
                if (tail == cur)
                    tail = item;
                ++size;
                return true;
            }
        }
        return false;
    }
    bool insert_before(const type &target, const type &val)
    {
        if (!head)
            return false;
        if (head->data == target)
        {
            Node<type> *item(new Node<type>(val));
            item->next = head;
            head = item;
            ++size;
            return true;
        }
        for (Node<type> *cur(head); cur->next; cur = cur->next)
        {
            if (cur->next->data == target)
            {
                Node<type> *item(new Node<type>(val));
                item->next = cur->next;
                cur->next = item;
                ++size;
                return true;
            }
        }
        return false;
    }
    type dequeue()
    {
        assert(!is_empty());
        type element(head->data);
        Node<type> *tmp(head);
        head = head->next;
        if (!head)
            tail = nullptr;
        delete tmp;
        --size;
        return element;
    }
    void print()
    {
        for (Node<type> *cur(head); cur; cur = cur->next)
            std::cout << cur->data << ' ';
        std::cout << edl;
    }
};

#endif