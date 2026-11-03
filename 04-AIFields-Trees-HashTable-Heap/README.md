# AI Fields: Trees, Hash Table and Heap

Stores AI fields and their subfields in nested binary search trees, then reuses the same field objects in a hash table and a min heap.

## How it works

1. Fields live in a binary search tree ordered by field name. Each node holds its own binary search tree of subfields ordered by subfield name. One generic `Tree<T>` class serves fields, subfields and words.
2. The whole tree is printed in alphabetical order, with the subfields of each field under it.
3. The field whose subfield tree is the tallest is found and printed with its subfields.
4. Every word from every definition goes into a third tree, stored in reverse alphabetical order with a counter, and is printed in alphabetical order with its frequency.
5. The fields are put into a `Dictionary` keyed by field name. The program asks for an existing field name and a new name, then renames the key.
6. The fields are also inserted into a hand written min heap. The first five are removed and printed with all their subfields.

## Data structures

- Generic binary search tree `Tree<T>`
- `Dictionary<string, Node<Field>>`
- Min heap on top of `List<Field>`

## Run

Open the `.sln` file in this folder with Visual Studio and press Ctrl+F5.

The program waits for keyboard input once. Type `Robotics`, press Enter, type `Autonomous Systems`, press Enter. The dictionary listing then shows `Autonomous Systems` instead of `Robotics`.

## Output (excerpt)

```
-Deepest Field-
Field Name: Computer Vision
Max Depth: 2

Subfield: Image Classification
Definition: Assigning a label to an entire image.
Applications: Medical imaging, object recognition, autonomous vehicles.

         -Word Frequency-         
Word: a                    Count: 6 
Word: actions              Count: 1 
Word: against              Count: 1 
Word: an                   Count: 2 
Word: and                  Count: 3 

DICTIONARY FIELDS:
Computer Vision
Generative AI
Machine Learning
Natural Language Processing
Autonomous Systems

HEAP DATA:

Computer Vision
```
