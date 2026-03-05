#ifndef STACK_LINKEDLIST_BASED
#define STACK_LINKEDLIST_BASED

#include <iostream>
#include <cassert>

#define edl '\n'

template <class T>
struct Node
{
    T data{};
    Node<T> *next{};
    Node(T val) : data(val) {}
};

template <class type>
class Stack_LinkedList_Based
{
    Node<type> *head{nullptr};
    int size{};

public:
    Stack_LinkedList_Based() {}
    ~Stack_LinkedList_Based()
    {
        while (!is_empty())
            pop();
        head = nullptr;
    }
    const int get_size() { return size; }
    bool is_empty() { return !head; }
    void push(type val)
    {
        Node<type> *item(new Node<type>(val));
        item->next = head;
        head = item;
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
            push(val);
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
    type pop()
    {
        assert(!is_empty());
        type element(head->data);
        Node<type> *tmp(head);
        head = head->next;
        delete tmp;
        return element;
    }
    type peek()
    {
        assert(!is_empty());
        return head->data;
    }
    void print()
    {
        for (Node<type> *cur(head); cur; cur = cur->next)
            std::cout << cur->data << ' ';
        std::cout << edl;
    }
};

#endif