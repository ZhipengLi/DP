def preorder_traverse(node, res):
    res.append(node.value)
    if node.left:
        preorder_traverse(node.left, res)
    if node.right:
        preorder_traverse(node.right, res)
class Node:
  def __init__(self, value, left=None, right=None):
    self.right = right
    self.left = left
    self.value = value

    self.parent = None

    if left:
      self.left.parent = self
    if right:
      self.right.parent = self

  def traverse_preorder(self):
    # todo - return inorder values (not Nodes)
    res = []
    preorder_traverse(self, res)
    return res
if __name__=="__main__":
    node = Node(1, Node(2), Node(3))
    for x in node.traverse_preorder():
        print(x)

    
    
