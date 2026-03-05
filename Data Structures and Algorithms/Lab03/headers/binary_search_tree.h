#ifndef Binary_Search_Tree_H
#define Binary_Search_Tree_H

#include <iostream>
#include <vector>
#include <cassert>
#include <queue>

#define edl '\n'

template <class T>
struct Node
{
    T data{};
    Node<T> *left{};
    Node<T> *right{};
    Node(T data) : data(data) {}
};

template <class type>
class Binary_Search_Tree
{
    Node<type> *root{};
    void insert_node(type val, Node<type> *node);
    void insert_node_iterative(type val, Node<type> *node);
    bool search_node(type val, Node<type> *node);
    void print_in_order_node(Node<type> *node);
    void print_pre_order_node(Node<type> *node);
    void print_post_order_node(Node<type> *node);
    void clear(Node<type> *node);
    Node<type> *min_node(Node<type> *node);
    Node<type> *max_node(Node<type> *node);
    Node<type> *delete_node(type target, Node<type> *node);
    bool is_bst(Node<type> *node);
    Node<type> *find_parent_node(type val, Node<type> *node, Node<type> *parent);

public:
    Binary_Search_Tree();
    ~Binary_Search_Tree();
    void insert_value(type val);
    void insert_value_iterative(type val);
    bool search(type val);
    void print_in_order();
    void print_pre_order();
    void print_post_order();
    type min_value();
    type max_value();
    void delete_value(type val);
    void level_order_traversal();
    type find_parent(type val);
};
#endif