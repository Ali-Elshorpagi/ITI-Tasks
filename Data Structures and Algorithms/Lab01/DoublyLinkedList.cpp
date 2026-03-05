#ifndef DOUBLY_LINKEDLIST_H
#define DOUBLY_LINKEDLIST_H

#include "Helper.cpp"

#define edl '\n'

template <class T>
struct Node
{
    T data{};
    Node<T> *next{}, *prev{};
    Node(T data) : data(data) {}
};

template <class type>
class Doubly_LinkedList
{
    Node<type> *head{}, *tail{};

    void link(Node<type> *first, Node<type> *second)
    {
        if (first)
            first->next = second;
        if (second)
            second->prev = first;
    }

    void delete_and_link(Node<type> *cur)
    {
        // nodeA -> nodeB -> nodeC -> nodeD
        if (cur == head)
            head = cur->next;

        if (cur == tail)
            tail = cur->prev;

        link(cur->prev, cur->next);

        delete cur;
    }

public:
    Doubly_LinkedList() = default;
    ~Doubly_LinkedList()
    {
        while (head)
        {
            Node<type> *current(head->next);
            delete head;
            head = current;
        }
        head = tail = nullptr;
    }
    void insert_end(type val)
    {
        Node<type> *item(new Node<type>(val));
        if (!head)
            head = tail = item;
        else
        {
            link(tail, item);
            tail = item;
        }
        tail->next = nullptr;
    }
    void print()
    {
        Helper::textattr(0x0B);
        int row(7);
        for (Node<type> *cur(head); cur; cur = cur->next)
        {
            Helper::gotoxy(20, row++);
            std::cout << cur->data << edl;
        }
        Helper::textattr(0x07);
    }
    bool delete_node_with_key(type val)
    {
        for (Node<type> *cur(head); cur; cur = cur->next)
        {
            if (cur->data == val)
            {
                delete_and_link(cur);
                return true;
            }
        }
        return false;
    }
    void delete_all_nodes_with_key(type val)
    {
        Node<type> *cur(head);
        while (cur)
        {
            if (cur->data == val)
            {
                Node<type> *temp(cur);
                cur = cur->next;
                delete_and_link(temp);
            }
            else
                cur = cur->next;
        }
    }
    int get_size()
    {
        int count(0);
        for (Node<type> *cur(head); cur; cur = cur->next)
            ++count;
        return count;
    }
    Node<type> *search(type val)
    {
        for (Node<type> *cur(head); cur; cur = cur->next)
        {
            if (cur->data == val)
                return cur;
        }
        return nullptr;
    }
    void delete_all()
    {
        while (head)
        {
            Node<type> *current(head->next);
            delete head;
            head = current;
        }
        head = tail = nullptr;
    }
    bool is_empty() { return head == nullptr; }
    Node<type> *get_head() { return head; }
};
#endif