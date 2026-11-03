# Deep Learning Fields

Parses a block of text that describes deep learning fields and their subfields into objects, then counts how many subfield definitions contain the word "learning".

## How it works

- The text is stored in a string. A line like `1) Name` starts a field, a line like `1.1 Name` starts a subfield, and the two lines after a subfield name are its definition and its applications.
- Fields are kept in a `Field[]`. Each `Field` holds a `List<Subfield>` and a `learningCount`.
- While filling the structure, every subfield whose definition contains "learning" (case insensitive) increases the counter of its field.

## Data structures

- `Field[]`
- `List<Subfield>`

## Run

Open the `.sln` file in this folder with Visual Studio and press Ctrl+F5.

## Output

```
2.Field's name: Recurrent Neural Networks (RNNs)
5.Field's 2.Subfield's Definition: Non-linear functions enabling neural networks to learn complex relationships.
6.Field's learning count: 2
Total learning count: 2
```
