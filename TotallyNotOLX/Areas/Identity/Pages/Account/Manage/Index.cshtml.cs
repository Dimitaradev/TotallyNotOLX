﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsModerator { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "First name")]
            public string FirstName { get; set; }
            [Display(Name = "Last name")]
            public string LastName { get; set; }
            [Display(Name = "Profile image")]
            public string ProfileImage { get; set; }
            [Display(Name = "Instagram")]
            public string Instagram { get; set; }
            [Display(Name = "Facebook")]
            public string Facebook { get; set; }
            [Display(Name = "Discord")]
            public string Discord { get; set; }
            [Display(Name = "Twitter")]
            public string Twitter { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;
            ProfileImage = user.ProfileImage;
            FirstName = user.FirstName;
            LastName = user.LastName;
            IsAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
            IsModerator = await _userManager.IsInRoleAsync(user, "Moderator");
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImage = user.ProfileImage,
                Discord = user.Discord,
                Instagram = user.Instagram,
                Facebook = user.Facebook,
                Twitter = user.Twitter
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var firstName = user.FirstName;
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                var setFirstNameResult = await _userManager.UpdateAsync(user);
                if (!setFirstNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set first name.";
                    return RedirectToPage();
                }
            }

            var lastName = user.LastName;
            if (Input.LastName != lastName)
            {
                user.LastName = Input.LastName;
                var setLastNameResult = await _userManager.UpdateAsync(user);
                if (!setLastNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set last name.";
                    return RedirectToPage();
                }
            }

            var profileImage = user.ProfileImage;
            if (Input.ProfileImage != profileImage)
            {
                user.ProfileImage = Input.ProfileImage;
                var setProfileImageResult = await _userManager.UpdateAsync(user);
                if (!setProfileImageResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set profile image.";
                    return RedirectToPage();
                }
            }

            var instagram = user.Instagram;
            if (Input.Instagram != instagram)
            {
                user.Instagram = Input.Instagram;
                var setInstagramResult = await _userManager.UpdateAsync(user);
                if (!setInstagramResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set instagram.";
                    return RedirectToPage();
                }
            }

            var discord = user.Discord;
            if (Input.Discord != discord)
            {
                user.Discord = Input.Discord;
                var setDiscordResult = await _userManager.UpdateAsync(user);
                if (!setDiscordResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set discord.";
                    return RedirectToPage();
                }
            }

            var facebook = user.Facebook;
            if (Input.Facebook != facebook)
            {
                user.Facebook = Input.Facebook;
                var setFacebookResult = await _userManager.UpdateAsync(user);
                if (!setFacebookResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set facebook.";
                    return RedirectToPage();
                }
            }

            var twitter = user.Twitter;
            if (Input.Twitter != twitter)
            {
                user.Twitter = Input.Twitter;
                var setTwitterResult = await _userManager.UpdateAsync(user);
                if (!setTwitterResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set twitter.";
                    return RedirectToPage();
                }
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
