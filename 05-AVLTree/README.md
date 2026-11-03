# AVL Tree

A self balancing binary search tree. After every insertion the heights are updated and the tree is rebalanced with rotations.

## How it works

- Each node stores its value and its height. A leaf has height 1 and an empty subtree has height 0.
- The balance difference of a node is the height of its left subtree minus the height of its right subtree. When it goes beyond 1 or below -1, one of the four cases is fixed:
  - left-left: right rotation
  - right-right: left rotation
  - left-right: left rotation on the left child, then right rotation
  - right-left: right rotation on the right child, then left rotation
- Duplicate values are ignored.
- The example inserts `16, 5, 6, 9, 21, 28, 2`, prints the keys in order and prints the height of the balanced tree.

## Data structures

- AVL tree

## Run

Open the `.sln` file in this folder with Visual Studio and press Ctrl+F5.

## Output

```
Tree Inorder: 2 5 6 9 16 21 28 
Balanced Tree Height: 4
```
