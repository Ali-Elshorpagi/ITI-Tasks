// #include "./headers/binary_search_tree.h"
#include "./sources/binary_search_tree.cpp"

void test_binary_search_tree()
{
    std::cout << "=== Testing Binary Search Tree ===" << edl << edl;

    Binary_Search_Tree<int> bst;

    std::cout << "Test 1: Inserting values 50, 30, 70, 20, 40, 60, 80" << edl;
    bst.insert_value(50), bst.insert_value(30), bst.insert_value(70), bst.insert_value(20), bst.insert_value(40), bst.insert_value(60), bst.insert_value(80);
    std::cout << "Values inserted successfully!" << edl << edl;

    std::cout << "Test 2: In-order traversal (should be sorted): ";
    bst.print_in_order();
    std::cout << edl << edl;

    std::cout << "Test 3: Pre-order traversal: ";
    bst.print_pre_order();
    std::cout << edl << edl;

    std::cout << "Test 4: Post-order traversal: ";
    bst.print_post_order();
    std::cout << edl << edl;

    std::cout << "Test 5: Level-order traversal:" << edl;
    bst.level_order_traversal();
    std::cout << edl;

    std::cout << "Test 6: Searching for values" << edl
              << "Search for 40: " << (bst.search(40) ? "Found" : "Not Found") << edl
              << "Search for 60: " << (bst.search(60) ? "Found" : "Not Found") << edl
              << "Search for 100: " << (bst.search(100) ? "Found" : "Not Found")
              << edl << edl;

    std::cout << "Test 7: Min and Max values" << edl
              << "Minimum value: " << bst.min_value() << edl
              << "Maximum value: " << bst.max_value() << edl
              << edl;

    std::cout << "Test 8: Finding parent nodes" << edl
              << "Parent of 20: " << bst.find_parent(20) << edl
              << "Parent of 40: " << bst.find_parent(40) << edl
              << "Parent of 30: " << bst.find_parent(30) << edl
              << "Parent of 70: " << bst.find_parent(70) << edl
              << "Parent of 50 (root): ";
    bst.find_parent(50);
    std::cout << "Parent of 100 (not in tree): ";
    bst.find_parent(100);
    std::cout << edl;

    std::cout << "Test 9: Deleting leaf node (20)" << edl;
    bst.delete_value(20);
    std::cout << "After deletion, In-order: ";
    bst.print_in_order();
    std::cout << edl << edl;

    std::cout << "Test 10: Deleting node with one child (30)" << edl;
    bst.delete_value(30);
    std::cout << "After deletion, In-order: ";
    bst.print_in_order();
    std::cout << edl << edl;

    std::cout << "Test 11: Deleting node with two children (50)" << edl;
    bst.delete_value(50);
    std::cout << "After deletion, In-order: ";
    bst.print_in_order();
    std::cout << edl << edl;

    std::cout << "Test 12: Level-order traversal after deletions:" << edl;
    bst.level_order_traversal();
    std::cout << edl;
}

int main()
{
    test_binary_search_tree();
    return (0);
}