using SocialApp.Structure;



namespace SocialApp.Grids
{
    public class clsTestGrid : absTextGrid
    {
        public override string Content { get; set; }

        protected override int ContentBoardHeight => 17;
        protected override int ContentBoardWidth => 1;
        protected override stBoarderInfo BoarderInfo => new stBoarderInfo('-', '|', '*');
        protected override stPaddingInfo PaddingInfo => new stPaddingInfo();
    }
}
