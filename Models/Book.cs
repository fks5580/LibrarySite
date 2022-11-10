using System.ComponentModel.DataAnnotations;

namespace LibrarySite.Models
{
    public class Book
    {
        /// <summary>
        /// 도서 번호
        /// </summary>
        [Key]
        public int BookNo { get; set; }

        /// <summary>
        /// 도서 제목
        /// </summary>
        [Required(ErrorMessage = "제목을 입력하세요.")]
        public string BookTitle { get; set; }

        /// <summary>
        /// 저자
        /// </summary>
        [Required(ErrorMessage = "저자를 입력하세요.")]
        public string BookWriter { get; set; }

        /// <summary>
        /// 요약
        /// </summary>
        public string BookSummary { get; set; }

        /// <summary>
        /// 출판사
        /// </summary>
        [Required(ErrorMessage = "출판사를 입력하세요.")]
        public string BookPublisher { get; set; }

        /// <summary>
        /// 출판일
        /// </summary>
        [Required(ErrorMessage = "출판일을 입력하세요.")]
        public string BookPublish_date { get; set; }

        /// <summary>
        /// 도서위치
        /// </summary>
        public string BookLocation { get; set; }

        /// <summary>
        /// 등록자
        /// </summary> 
        [Required]
        public int UserNo { get; set; }
    }
}
