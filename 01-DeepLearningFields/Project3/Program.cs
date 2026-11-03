using Project3;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{

    class Subfield //Subfield class for name, definition and application of subfield.
    {
       public string name;
       public string def;
       public string appl;

       public Subfield(string name,string def, string appl)
        {
            this.name = name;
            this.def = def;
            this.appl = appl;
        }
    }

    class Field //Field class for name, list of subfields and learning count value.
    {
        public string name;
        public List<Subfield> subfields;
        public int learningCount;

        public Field(string name)
        {
            this.name = name;
            this.subfields = new List<Subfield>();
            this.learningCount = 0;
        }
    }

    internal class Program
    {
        public static Field[] createArray(string text) //Creating a Field-type array from given text
        {
            int fieldNum = 0;

            string[] lines=text.Split('\n');
            for(int i=0; i<lines.Length; i++)
            {
                string line = lines[i];

                if (line.Trim() == "")
                {
                    continue;
                } 

                if (line.Length>1 && line[1] == ')') //Fields start with "1) ", "2) "...
                {
                    fieldNum++;
                }
            }
 
            Field[] fields = new Field[fieldNum];
            return fields;

        }
        
        public static void fillArray(Field[] fields, string text) //Filling the array structure with given data.
        {
            int fieldIndex = 0;
            Field currentField= null;
            Subfield currentSubfield= null;

            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];


                if (line.Trim() == "")
                {
                    continue;
                }
                if (line.Length > 1 && line[1] == ')')
                {
                    int startIdx = line.IndexOf(')') + 2;
                    string fieldName = line.Substring(startIdx);
                    currentField = new Field(fieldName);
                    fields[fieldIndex] = currentField;
                    fieldIndex++;
                }

                else
                {
                    int subStartIdx = line.IndexOf(" ") + 1; //Subfields start with "1.1 ", "2.1 "...
                    string subfieldName = line.Substring(subStartIdx);

                    int defLineIdx = i + 1;
                    while (defLineIdx < lines.Length && lines[defLineIdx].Trim() == "")
                    {
                        defLineIdx++;
                    }
                    string def = defLineIdx < lines.Length ? lines[defLineIdx] : "";
                    
                 
                    int appLineIdx = defLineIdx + 1;
                    while (appLineIdx < lines.Length && lines[appLineIdx].Trim() == "")
                    {
                        appLineIdx++;
                    }
                    string appl = appLineIdx < lines.Length ? lines[appLineIdx] : "";

                    currentSubfield = new Subfield(subfieldName, def, appl);

                    if (def.IndexOf("learning",StringComparison.OrdinalIgnoreCase)>=0) //Contains learning check
                    {
                        currentField.learningCount++;
                    }
                    currentField.subfields.Add(currentSubfield);
                    i = appLineIdx;
                }

            }
        }

        public static int totalLearningCount(Field[] fields) //Getting the total learning count in all fields' defs.
        {
            int total = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                total += fields[i].learningCount;
            }
            return total;
        }

        static void Main(string[] args)
        {
            string text = "1) Convolutional Neural Networks (CNNs)\r\n1.1 Convolution Layers\r\n\r\nDefinition: Layers that use learnable filters to extract spatial features such as edges, textures, and shapes.\r\nApplications: Image classification, object detection, medical imaging.\r\n\r\n1.2 Pooling Layers\r\n\r\nDefinition: Downsampling operations that reduce spatial dimension and noise.\r\nApplications: Faster inference, dimensionality reduction, feature generalization.\r\n\r\n1.3 Residual Networks (ResNet)\r\n\r\nDefinition: Architectures that use skip connections to prevent vanishing gradients in deep networks.\r\nApplications: High-accuracy vision models, backbone networks for detection/segmentation.\r\n\r\n1.4 Batch Normalization\r\n\r\nDefinition: Normalizes layer activations to stabilize and accelerate training.\r\nApplications: Deep CNN training, regularization, improved convergence.\r\n\r\n1.5 Fully Convolutional Networks (FCN)\r\n\r\nDefinition: CNN models that replace fully connected layers with convolutions for dense predictions.\r\nApplications: Semantic segmentation, medical imaging, heatmap prediction.\r\n\r\n2) Recurrent Neural Networks (RNNs)\r\n2.1 LSTM\r\n\r\nDefinition: Gated recurrent units designed to capture long-term dependencies in sequences.\r\nApplications: Speech recognition, time series forecasting, NLP tasks.\r\n\r\n2.2 GRU\r\n\r\nDefinition: Lightweight gated recurrent units with fewer parameters than LSTM.\r\nApplications: Real-time NLP, mobile models, medium-length dependencies.\r\n\r\n2.3 Bidirectional RNN\r\n\r\nDefinition: Processes input sequences in both forward and backward directions for richer context.\r\nApplications: Named entity recognition, sentiment analysis, sequence labeling.\r\n\r\n3) Transformer Models\r\n3.1 Encoder Models (BERT family)\r\n\r\nDefinition: Bidirectional transformer encoders producing contextual embeddings.\r\nApplications: Classification, question answering, semantic similarity.\r\n\r\n3.2 Decoder Models (GPT family)\r\n\r\nDefinition: Autoregressive transformers generating outputs token-by-token.\r\nApplications: Chatbots, text generation, code generation.\r\n\r\n3.3 Sequence-to-Sequence Models\r\n\r\nDefinition: Encoder–decoder transformers designed for mapping sequences to sequences.\r\nApplications: Translation, summarization, text rewriting.\r\n\r\n3.4 Vision Transformers (ViT)\r\n\r\nDefinition: Transformer models that process images as patch sequences instead of using convolutions.\r\nApplications: Image classification, image segmentation, self-supervised learning.\r\n\r\n4) Generative Models\r\n4.1 GANs\r\n\r\nDefinition: Two-network adversarial framework where a generator competes against a discriminator to produce realistic samples.\r\nApplications: Face synthesis, style transfer, synthetic data creation.\r\n\r\n4.2 Diffusion Models\r\n\r\nDefinition: Models that generate data by iteratively denoising random noise.\r\nApplications: AI art, image editing, high-fidelity image generation.\r\n\r\n4.3 VAE (Variational Autoencoders)\r\n\r\nDefinition: Latent-variable generative models that learn probabilistic encodings and decodings.\r\nApplications: Data generation, anomaly detection, latent space exploration.\r\n\r\n4.4 Autoregressive Image Models (PixelCNN, PixelRNN)\r\n\r\nDefinition: Models that generate images pixel-by-pixel using conditional probability distributions.\r\nApplications: Density modeling, low-level generative tasks.\r\n\r\n5) Feedforward Neural Networks (FNN / MLP)\r\n5.1 Multilayer Perceptrons\r\n\r\nDefinition: Fully connected feedforward networks mapping inputs to outputs through learned weights.\r\nApplications: Tabular data tasks, basic classification/regression.\r\n\r\n5.2 Activation Functions\r\n\r\nDefinition: Non-linear functions enabling neural networks to learn complex relationships.\r\nApplications: All deep learning architectures (CNNs, RNNs, Transformers).\r\n\r\n6) Deep Reinforcement Learning\r\n6.1 DQN\r\n\r\nDefinition: Deep Q-learning combining Q-value updates with neural network function approximation.\r\nApplications: Atari games, discrete control, navigation tasks.\r\n\r\n6.2 Policy Gradient Methods\r\n\r\nDefinition: Methods that directly optimize the parameters of a policy using gradient ascent on expected reward.\r\nApplications: Robotics, continuous control, complex movement tasks.\r\n\r\n6.3 Actor–Critic Methods\r\n\r\nDefinition: Framework combining a policy network (actor) and a value estimator (critic) for stable learning.\r\nApplications: Multi-step decision making, real-time control.\r\n\r\n6.4 Model-based RL\r\n\r\nDefinition: Methods that learn an internal model of the environment to improve sample efficiency.\r\nApplications: Robotics, planning, simulation-based training.";
            Field[] fields = createArray(text);
            fillArray(fields, text);

            //Printing some data from the structure
            Console.WriteLine("2.Field's name: "+fields[1].name);
            Console.WriteLine("5.Field's 2.Subfield's "+fields[4].subfields[1].def);
            Console.WriteLine("6.Field's learning count: "+fields[5].learningCount);
            Console.WriteLine("Total learning count: "+totalLearningCount(fields));
        
            
        }
    }
}