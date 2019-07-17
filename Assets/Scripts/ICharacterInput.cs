
public interface ICharacterInput{
    void ReadInput();
    float Horizontal { get;   }
    bool Attack { get;   }
    bool Jump { get;  }
    bool HeldJump { get; }
}
