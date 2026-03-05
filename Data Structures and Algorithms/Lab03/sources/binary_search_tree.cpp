#ifndef Binary_Search_Tree_CPP
#define Binary_Search_Tree_CPP

#include "..\headers\binary_search_tree.h"

template <class type>
Binary_Search_Tree<type>::Binary_Search_Tree() {}

template <class type>
Binary_Search_Tree<type>::~Binary_Search_Tree()
{
    clear(root);
}

template <class type>
void Binary_Search_Tree<type>::clear(Node<type> *node)
{
    if (node)
    {
        clear(node->left);
        clear(node->right);
        delete node;
    }
}

template <class type>
Node<type> *Binary_Search_Tree<type>::delete_node(type target, Node<type> *node)
{
    if (!node)
        return nullptr;
    if (target < node->data)
        node->left = delete_node(target, node->left);
    else if (target > node->data)
        node->right = delete_node(target, node->right);
    else
    {
        Node<type> *tmp(node);
        if (!node->left && !node->right) // case 1: no child
            node = nullptr;
        else if (!node->right) // case 2: has left only
            node = node->left; // connect with child
        else if (!node->left)  // case 2: has right only
            node = node->right;
        else // 2 children: Use successor
        {
            Node<type> *mn(min_node(node->right));
            node->data = mn->data; // copy & go delete
            node->right = delete_node(node->data, node->right);
            tmp = nullptr; // Don't delete me. Successor will be deleted
        }
        if (tmp)
            delete tmp;
    }
    return node;
}

template <class type>
Node<type> *Binary_Search_Tree<type>::min_node(Node<type> *node)
{
    while (node && node->left)
        node = node->left;
    return node;
}

template <class type>
Node<type> *Binary_Search_Tree<type>::max_node(Node<type> *node)
{
    while (node && node->right)
        node = node->right;
    return node;
}

template <class type>
void Binary_Search_Tree<type>::insert_node(type val, Node<type> *node)
{
    if (val < node->data)
    {
        if (!node->left)
            node->left = new Node<type>(val);
        else
            insert_node(val, node->left);
    }
    else if (val > node->data)
    {
        if (!node->right)
            node->right = new Node<type>(val);
        else
            insert_node(val, node->right);
    }
}

template <class type>
void Binary_Search_Tree<type>::insert_node_iterative(type val, Node<type> *node)
{
    Node<type> *current(node), *parent(nullptr);

    while (current)
    {
        parent = current;
        if (val < current->data)
            current = current->left;
        else if (val > current->data)
            current = current->right;
        else
            return; // exist
    }

    if (val < parent->data)
        parent->left = new Node<type>(val);
    else
        parent->right = new Node<type>(val);
}

template <class type>
bool Binary_Search_Tree<type>::search_node(type val, Node<type> *node)
{
    if (!node)
        return false;
    if (val == node->data)
        return true;
    if (val < node->data)
        return search_node(val, node->left);
    return search_node(val, node->right);
}

template <class type>
void Binary_Search_Tree<type>::print_in_order_node(Node<type> *node)
{
    if (!node)
        return;
    print_in_order_node(node->left);
    std::cout << node->data << ' ';
    print_in_order_node(node->right);
}

template <class type>
void Binary_Search_Tree<type>::print_pre_order_node(Node<type> *node)
{
    if (!node)
        return;
    std::cout << node->data << ' ';
    print_in_order_node(node->left);
    print_in_order_node(node->right);
}

template <class type>
void Binary_Search_Tree<type>::print_post_order_node(Node<type> *node)
{
    if (!node)
        return;
    print_in_order_node(node->left);
    print_in_order_node(node->right);
    std::cout << node->data << ' ';
}

template <class type>
bool Binary_Search_Tree<type>::is_bst(Node<type> *node)
{
    bool left_bst(!node->left || (node->data > node->left->data) && is_bst(node->left));
    if (!left_bst)
        return false;
    bool right_bst(!node->right || (node->data < node->right->data) && is_bst(node->right));
    return right_bst;
}

template <class type>
void Binary_Search_Tree<type>::insert_value(type val)
{
    if (!root)
        root = new Node<type>(val);
    else
        insert_node(val, root);
    is_bst(root);
}

template <class type>
void Binary_Search_Tree<type>::insert_value_iterative(type val)
{
    if (!root)
        root = new Node<type>(val);
    else
        insert_node_iterative(val, root);
    is_bst(root);
}

template <class type>
bool Binary_Search_Tree<type>::search(type val)
{
    return search_node(val, root);
}

template <class type>
void Binary_Search_Tree<type>::print_in_order()
{
    print_in_order_node(root);
}

template <class type>
void Binary_Search_Tree<type>::print_pre_order()
{
    print_pre_order_node(root);
}

template <class type>
void Binary_Search_Tree<type>::print_post_order()
{
    print_post_order_node(root);
}

template <class type>
type Binary_Search_Tree<type>::min_value()
{
    return min_node(root)->data;
}

template <class type>
type Binary_Search_Tree<type>::max_value()
{
    return max_node(root)->data;
}

template <class type>
void Binary_Search_Tree<type>::delete_value(type val)
{
    if (root)
        root = delete_node(val, root), is_bst(root);
}

template <class type>
void Binary_Search_Tree<type>::level_order_traversal()
{
    if (!root)
        return;
    std::cout << "********************" << edl;
    std::queue<Node<type> *> nodes_queue;
    nodes_queue.push(root);
    int level(0);
    while (!nodes_queue.empty())
    {
        int sze(nodes_queue.size());
        std::cout << "Level " << level << ": ";
        while (sze--)
        {
            Node<type> *cur(nodes_queue.front());
            nodes_queue.pop();
            std::cout << cur->data << ' ';
            if (cur->left)
                nodes_queue.push(cur->left);
            if (cur->right)
                nodes_queue.push(cur->right);
        }
        ++level;
        std::cout << edl;
    }
}

template <class type>
Node<type> *Binary_Search_Tree<type>::find_parent_node(type val, Node<type> *node, Node<type> *parent)
{
    if (!node)
        return nullptr;
    if (node->data == val)
        return parent;
    if (val < node->data)
        return find_parent_node(val, node->left, node);
    return find_parent_node(val, node->right, node);
}

template <class type>
type Binary_Search_Tree<type>::find_parent(type val)
{
    if (!root)
    {
        std::cout << "the Tree is Empty" << edl;
        return type();
    }
    if (root->data == val)
    {
        std::cout << "this is the Root which has no parent" << edl;
        return type();
    }
    Node<type> *parent(find_parent_node(val, root, nullptr));
    if (parent)
        return parent->data;
    std::cout << "Value not found in tree" << edl;
    return type();
}

#endif
